namespace Display
{
    /// <summary>
    /// States for the cells in the grid. Each cell state is associated with a 
    /// different character when rendering cells on the grid.
    /// </summary>
    /// <author>Benjamin Lewis</author>
    /// <date>August 2020</date>
    public enum CellState
    {
        Blank,
        Full,
        Dark,
        Medium,
        Light
    }

    /// <summary>
    /// Grid cell used to store data in the grid.
    /// </summary>
    /// <author>Benjamin Lewis</author>
    /// <date>August 2020</date>
    public class Cell
    {
        private readonly char[] RENDER_CHARACTERS = new char[] {
            '\u0020',   // ' '
            '\u2588',   // '█'
            '\u2593',   // '▓'
            '\u2592',   // '▒'
            '\u2591'    // '░'
        };

        private CellState state;

        /// <summary>
        /// Initializes the cell (as blank)
        /// </summary>
        public Cell()
        {
            state = CellState.Blank;
        }

        /// <summary>
        /// Sets the state of the cell
        /// </summary>
        /// <param name="state">The new state of the cell</param>
        public void SetState(CellState state)
        {
            this.state = state;
        }

        /// <summary>
        /// Writes the cell to the buffer at the specified buffer coordinates 
        /// and cell size using the cells state character. 
        /// </summary>
        /// <param name="buffer">The render buffer.</param>
        /// <param name="row">The top left buffer row index.</param>
        /// <param name="col">The top left buffer rolumn index.</param>
        /// <param name="width">The number of characters to draw horizontally.</param>
        /// <param name="height">The number of characters to draw vertically.</param>
        public void Draw(ref char[][] buffer, int row, int col, int width, int height)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    buffer[row + i][col + j] = RENDER_CHARACTERS[(int)state];
                }
            }
        }

    }
}
