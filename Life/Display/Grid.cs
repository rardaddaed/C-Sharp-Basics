using System;
using System.Runtime.InteropServices;

namespace Display
{
    /// <summary>
    /// Statically sized console window with a programmable grid. Updates to 
    /// the grid can be specified and rendered separately when requested.
    /// </summary>
    /// <author>Benjamin Lewis</author>
    /// <date>August 2020</date>
    public class Grid
    {
        private const int TopMargin = 1;
        private const int BottomMargin = 0;
        private const int LeftMargin = 2;
        private const int RightMargin = 0;
        private const int Border = 1;
        private const int CellWidth = 2;
        private const int CellHeight = 1;
        private const int MinSize = 4;
        private const int MaxSize = 48;
        private const int MinState = 0;
        private const int NumStates = 5;

        private int storedWindowWidth;
        private int storedWindowHeight;
        private int storedBufferWidth;
        private int storedBufferHeight;

        private int rows;
        private int cols;
        private int bufferHeight;
        private int bufferWidth;
        private string footnote;
        private Cell[][] cells;
        private char[][] buffer;

        public bool IsComplete { get; set; }

#if WINDOWS
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
#else
        [DllImport("libc")]
        private static extern int system(string exec);
#endif

        /// ------------------------------------------------------------
        /// Public Methods. These CAN be called from your program.
        /// ------------------------------------------------------------

        /// <summary>
        /// Constructs the grid with specified dimensions.
        /// </summary>
        /// <param name="rows">The number of rows in the grid.</param>
        /// <param name="cols">The number of columns in the grid.</param>
        /// /// <exception cref="System.ArgumentOutOfRangeException">
        /// Thrown when the number of rows/columns specified lies outside the acceptable range of values.
        /// </exception>
        public Grid(int rows, int cols)
        {
            if (rows < MinSize || rows > MaxSize)
            {
                throw new ArgumentOutOfRangeException($"The number of grid rows is not within the acceptable range " +
                    $"of values ({MinSize} to {MaxSize}).");
            }

            if (cols < MinSize || cols > MaxSize)
            {
                throw new ArgumentOutOfRangeException($"The number of grid columns is not within the acceptable range " +
                    $"of values ({MinSize} to {MaxSize}).");
            }

            this.rows = rows;
            this.cols = cols;
            this.IsComplete = false;

            CalculateBufferSize();
            InitializeCells();
            InitializeBuffer();
            DrawBorder();
        }

        /// <summary>
        /// Resizes the window to the appropriate size and clears the console.
        /// </summary>
        public void InitializeWindow()
        {
            Console.Clear();

            storedWindowWidth = Console.WindowWidth;
            storedWindowHeight = Console.WindowHeight;
            storedBufferWidth = Console.BufferWidth;
            storedBufferHeight = Console.BufferHeight;

            Console.CursorVisible = false;

#if WINDOWS
            Console.SetWindowSize(bufferWidth + 1, bufferHeight + 1);
            Console.SetBufferSize(bufferWidth + 1, bufferHeight + 1);
#else
            system($@"printf '\e[8;{bufferHeight + 1};{bufferWidth + 1}t'");
#endif

            Console.Clear();
        }

        /// <summary>
        /// Reverts the window back to it's original state and clears the console.
        /// </summary>
        public void RevertWindow()
        {
            Console.Clear();

            Console.CursorVisible = true;

#if WINDOWS
            Console.SetWindowSize(storedWindowWidth, storedWindowHeight);
            Console.SetBufferSize(storedBufferWidth, storedBufferHeight);
#else
            system($@"printf '\e[8;{storedBufferHeight};{storedBufferWidth}t'");
#endif

            Console.Clear();
        }

        /// <summary>
        /// Updates the state of a cell at specified grid coordinates.
        /// </summary>
        /// <param name="row">The grid row index.</param>
        /// <param name="col">The grid column index.</param>
        /// <param name="state">The new state of the cell.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// Thrown when the row/column indices are outside of the boundaries of the grid or when an
        /// invalid cell state is specified.
        /// </exception>
        public void UpdateCell(int row, int col, CellState state)
        {
            if (0 > row || row >= rows)
            {
                throw new ArgumentOutOfRangeException("The row index exceeds the bounds of the grid.");
            }

            if (0 > col || col >= cols)
            {
                throw new ArgumentOutOfRangeException("The column index exceeds the bounds of the grid.");
            }

            if (MinState > (int)state || (int)state >= MinState + NumStates)
            {
                throw new ArgumentOutOfRangeException("The specified state is invalid (does not exist).");
            }

            cells[row][col].SetState(state);
            cells[row][col].Draw(ref buffer, CellRowOffset(row), CellColOffset(col), CellWidth, CellHeight);
        }

        /// <summary>
        /// Renders the current state of the grid (all updates applied after the last render will be rendered).
        /// </summary>
        public void Render()
        {
            Console.SetCursorPosition(0, TopMargin);
            string render = "";
            for (int row = TopMargin; row < bufferHeight; row++)
            {
                render += new string(buffer[row]) + "\n";
            }
            Console.Write(render);
            Console.Write(footnote.PadLeft(LeftMargin + Border + cols * CellWidth));
            if (IsComplete)
            {
                Console.SetCursorPosition(LeftMargin + Border + (cols * CellWidth) / 2 - 5,
                                          TopMargin + Border + rows * CellHeight);
                Console.Write(" COMPLETE ");
            }
        }

        /// <summary>
        /// Sets the footnote that appears to the bottom left of the grid. If the footnote is too large,
        /// it is truncated to fill the width of the grid (not including borders). Truncation starts at the 
        /// left of the string.
        /// </summary>
        /// <param name="footnote">The footnote to be displayed</param>
        public void SetFootnote(string footnote)
        {
            if (footnote.Length > CellWidth * cols)
            {
                this.footnote = footnote.Substring(footnote.Length - CellWidth * cols, CellWidth * cols);
            }
            else
            {
                this.footnote = footnote;
            }
        }

        /// ------------------------------------------------------------
        /// Private Methods. These CANNOT be called from your program.
        /// ------------------------------------------------------------

        /// <summary>
        /// Initializes the cell array to be filled with blank cells.
        /// </summary>
        private void InitializeCells()
        {
            cells = new Cell[rows][];
            for (int i = 0; i < rows; i++)
            {
                cells[i] = new Cell[cols];
                for (int j = 0; j < cols; j++)
                {
                    cells[i][j] = new Cell();
                }
            }
        }

        /// <summary>
        /// Initializes the buffer array to be filled with whitespace.
        /// </summary>
        private void InitializeBuffer()
        {
            buffer = new char[bufferHeight][];
            for (int i = 0; i < bufferHeight; i++)
            {
                buffer[i] = new char[bufferWidth];
                for (int j = 0; j < bufferWidth; j++)
                {
                    buffer[i][j] = ' ';
                }
            }
        }

        /// <summary>
        /// Draws border characters at the appropriate buffer locations.
        /// </summary>
        private void DrawBorder()
        {
            if (Border == 1)
            {
                buffer[TopMargin][LeftMargin] = '╔';
                buffer[TopMargin][LeftMargin + Border + CellWidth * cols] = '╗';
                buffer[TopMargin + Border + CellHeight * rows][LeftMargin] = '╚';
                buffer[TopMargin + Border + CellHeight * rows][LeftMargin + Border + CellWidth * cols] = '╝';
                for (int i = TopMargin + Border; i <= (TopMargin + Border * CellHeight * rows); i++)
                {
                    buffer[i][LeftMargin] = '║';
                    buffer[i][LeftMargin + Border + CellWidth * cols] = '║';
                }
                for (int j = LeftMargin + Border; j <= (LeftMargin + Border * CellWidth * cols); j++)
                {
                    buffer[TopMargin][j] = '═';
                    buffer[TopMargin + Border + CellHeight * rows][j] = '═';
                }
            }
        }

        /// <summary>
        /// Offsets a grid column index to a buffer column index with respect
        /// to the left margin, border and cell width.
        /// </summary>
        /// <param name="col">The grid column index</param>
        /// <returns>The offset buffer column index</returns>
        private int CellColOffset(int col)
        {
            return CellWidth * col + LeftMargin + Border;
        }

        /// <summary>
        /// Offsets a grid row index to a buffer row index with respect
        /// to the top margin, border and cell height.
        /// </summary>
        /// <param name="row">The grid row index</param>
        /// <returns>The offset buffer row index</returns>
        private int CellRowOffset(int row)
        {
            return (rows - 1) - CellHeight * row + TopMargin + Border;
        }

        /// <summary>
        /// Calculates the buffer size based on margins borders and cell counts/sizes.
        /// </summary>
        private void CalculateBufferSize()
        {
            bufferHeight = TopMargin + BottomMargin + 2 * Border + CellHeight * rows;
            bufferWidth = LeftMargin + RightMargin + 2 * Border + CellWidth * cols;
        }
    }
}
