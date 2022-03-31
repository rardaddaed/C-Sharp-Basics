using Display;
using Life.CellType;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Life
{
    /// <summary>
    ///  Main class which initialise the game board with either randomised cells or seeded ones if seed files are entered. 
    ///  It then applies the rules of Life to the cells and creates the grid for the next generation and so on. 
    /// </summary>
    /// <author>Jinyu Li</author>
    /// <Date>September 2020</Date>
    public class Life
    {
        public BaseLifeCell[] LifeCells;
        public Queue<BaseLifeCell[]> LifeHistories = new Queue<BaseLifeCell[]>();
        public int Rows;
        public int Cols;
        public bool Periodic;
        public float Random;
        public int Generations;
        public float Update;
        public bool Step;
        public string Filename;

        public string Type = "N/A";
        public int Order = 0;
        public bool CentreCount = false;
        public int Memory = 16;
        public int countGen;
        public bool nonPeriodic = false;
        public List<int> Survivals = new List<int>() { 2, 3 };
        public List<int> Births = new List<int>() { 3 };
        public bool Ghost = false;

        private Grid grid;
        private Random rand;
        private Log log;

        /// <summary>
        /// Constructor for the class, parses all values required for the program.
        /// Then constructs the grid with according sizes and generates an array of empty cells.
        /// Or generates an array of cells that are assigned in a seed file.
        /// </summary>
        public Life(int rows, int cols, bool periodic, float random, int generations, float update, bool step, string filename, 
            Grid grid, string type, int order, bool centreCount, List<int> survivals, List<int> births, int memory, bool ghost)
        {
            // Input values
            Rows = rows;
            Cols = cols;
            Periodic = periodic;
            Random = random;
            Generations = generations;
            Update = update;
            Step = step;
            Filename = filename;

            Type = type;
            Order = order;
            CentreCount = centreCount;
            Survivals = survivals;
            Births = births;
            Memory = memory;
            Ghost = ghost;

            rand = new Random();
            log = new Log();
            List<BaseLifeCell> seededCells = new List<BaseLifeCell>();

            // Read the seed file if it is imported to the program
            if (Filename != "N/A")
            {
                seededCells = ReadSeedFile(Filename, order, type);
            }

            // Array of cells with according size
            this.grid = grid;
            LifeCells = new BaseLifeCell[rows * cols];

            // Generates empty avlues for every cells in the array
            // Or give the values of assigned cells in a seed file to the array
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    BaseLifeCell foundCell = seededCells.Find(c => c.X == i && c.Y == j);

                    // When no seed file is imported
                    if (foundCell == null)
                    {
                        BaseLifeCell cell;
                        if (type == "moore")
                        {
                            cell = new LifeCellMoore(i, j, order);
                        } else
                        {
                            cell = new LifeCellVon(i, j, order);
                        }
                        LifeCells[i * cols + j] = cell;
                    }

                    // When a seed file is imported
                    else
                    {
                        LifeCells[i * cols + j] = foundCell;
                    }
                }
            }
        }
        
        /// <summary>
        /// Method to read a seed file and store its values in a list of cells.
        /// Also displays a warning message when the game board is too small to display the cells.
        /// </summary>
        /// <returns>Returns a list of lifecells that is full in the seeded files</returns>
        private List<BaseLifeCell> ReadSeedFile(string filename, int order, string type)
        {
            string[] lines = File.ReadAllLines(filename);
            List<BaseLifeCell> lifeCells = new List<BaseLifeCell>();
            bool outBoundaryMark = false;
            int maxX = 0;
            int maxY = 0;

            // Loop the information contained in seed files, determine if a cell is out of boundary
            // and add the appropriate ones to the list.
            if (lines.Length > 0 && lines[0] == "#version=1.0")
            {
                foreach (string line in lines)
                {
                    // Skip the first line
                    if (!line.StartsWith("#version"))
                    {
                        string[] coords = line.Split(' ');
                        int oriX = int.Parse(coords[0]);
                        int x = Rows - 1 - oriX;
                        int y = int.Parse(coords[1]);
                        if (x >= Rows || y >= Cols)
                        {
                            if (oriX > maxX) maxX = oriX;
                            if (y > maxY) maxY = y;
                            outBoundaryMark = true;
                        }
                        else
                        {
                            BaseLifeCell cell;
                            if (type == "moore")
                            {
                                cell = new LifeCellMoore(x, y, CellState.Full, order);
                            }
                            else
                            {
                                cell = new LifeCellVon(x, y, CellState.Full, order);
                            }
                            lifeCells.Add(cell);
                        }
                    }
                }
            } else if(lines.Length > 0 && lines[0] == "#version=2.0")
            {
                foreach (string line in lines)
                {
                    // Skip the first line
                    if (!line.StartsWith("#version"))
                    {
                        string status = line.Substring(line.IndexOf('(') + 1, line.IndexOf(')') - line.IndexOf('(') - 1);
                        string cellType = line.Substring(line.IndexOf(')') + 1, line.IndexOf(':') - line.IndexOf(')') - 1).Trim().ToLower();
                        string coords = line.Substring(line.IndexOf(':') + 1, line.Length - line.IndexOf(':') - 1);
                        string[] coordsArr = coords.Split(',');
                        int[] coordsInt = new int[coordsArr.Length];
                        for (int i = 0; i < coordsArr.Length; i++)
                        {
                            coordsInt[i] = int.Parse(coordsArr[i].Trim());
                        }

                        BaseLifeCellType lifeCellType;
                        switch (cellType)
                        {
                            case "rectangle":
                                lifeCellType = new LifeCellTypeRectangle(coordsInt);
                                break;
                            case "ellipse":
                                lifeCellType = new LifeCellTypeEllipse(coordsInt);
                                break;
                            default:
                                lifeCellType = new LifeCellType(coordsInt);
                                break;
                        }

                        foreach (var cellCoord in lifeCellType.LifeCellCoords)
                        {
                            int x = Rows - 1 - cellCoord.X;
                            if (x >= Rows || cellCoord.Y >= Cols)
                            {
                                if (cellCoord.X > maxX) maxX = cellCoord.X;
                                if (cellCoord.Y > maxY) maxY = cellCoord.Y;
                                outBoundaryMark = true;
                            }
                            else
                            {
                                BaseLifeCell cell;
                                if (type == "moore")
                                {
                                    cell = new LifeCellMoore(x, cellCoord.Y, status == "o" ? CellState.Full : CellState.Blank, order);
                                }
                                else
                                {
                                    cell = new LifeCellVon(x, cellCoord.Y, status == "o" ? CellState.Full : CellState.Blank, order);
                                }
                                var foundCell = lifeCells.Find(c => c.X == cell.X && c.Y == cell.Y);
                                if (foundCell == null)
                                    lifeCells.Add(cell);
                                else
                                    foundCell.State = cell.State;
                            }
                        }
                    }
                }
            }

            // Display warning
            if (outBoundaryMark)
                log.Warning($"Seeded cells are outside of bounds of the universe!\nRecommended universe size: {maxX} x {maxY}");

            return lifeCells;
        }

        /// <summary>
        /// Method to initialise the game board, fill it with randomised cells or seeded ones.
        /// </summary>
        public void Initialise()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    // Get the cellstate of every cell in array
                    CellState cellState = Array.Find(LifeCells, c => c.X == i && c.Y == j).State;

                    // Generate random cellstates for every cell with assigned probability if no seed file is imported
                    if (Filename == "N/A")
                    {
                        cellState = CellState.Blank;
                        if (rand.Next(0, 100) <= Random * 100)
                        {
                            cellState = CellState.Full;
                        }
                    }

                    // Display cells and update array
                    grid.UpdateCell(Rows - 1 - i, j, cellState);
                    Array.Find(LifeCells, c => c.X == i && c.Y == j).State = cellState;
                }
            }
        }

        /// <summary>
        /// Method to evolve the game from initialisation by applying the rules of Life
        /// </summary>
        public void Evolve()
        {
            // Create a tempurary array to pass on the lifecell state to the next iteration of life

            BaseLifeCell[] tempCells = new BaseLifeCell[Rows * Cols];

            for (int i = 0; i < LifeCells.Length; i++) {
                tempCells[i] = (BaseLifeCell)LifeCells[i].Clone();
            }

            if (LifeHistories.Count >= 3)
            {
                LifeHistories.Dequeue();
                LifeHistories.Enqueue(tempCells);
            } else
            {
                LifeHistories.Enqueue(tempCells);
            }

            // Apply rules of Life to each cells, apply the periodic rules if periodic mode is on
            foreach (var cell in LifeCells)
            {
                // Counts live neighbours for each cell, depending on the periodic mode
                int countLive = Periodic ? cell.CountLiveNeighboursPeriodic(LifeCells, Rows, Cols, CentreCount) : cell.CountLiveNeighbours(LifeCells, Rows, Cols, CentreCount);

                //Update the array
                var foundNotBlankCell = Array.Find(tempCells, c => c.X == cell.X && c.Y == cell.Y && c.State != CellState.Blank);
                if (foundNotBlankCell != null)
                {
                    CellState changedState = foundNotBlankCell.ChangeToDead(Ghost);
                    grid.UpdateCell(Rows - 1 - cell.X, cell.Y, changedState);
                }
                else
                {
                    Array.Find(tempCells, c => c.X == cell.X && c.Y == cell.Y).State = CellState.Blank;
                    grid.UpdateCell(Rows - 1 - cell.X, cell.Y, CellState.Blank);
                }

                //grid.UpdateCell(Rows - 1 - cell.X, cell.Y, CellState.Blank);
                //Array.Find(tempCells, c => c.X == cell.X && c.Y == cell.Y).State = CellState.Blank;

                // Apply the rule for dead cells and update array
                for (int i  = 0; i < Births.Count; i++)
                {
                    if (countLive == Births[i] && (cell.State == CellState.Blank || cell.State == CellState.Dark || cell.State == CellState.Medium
                        || cell.State == CellState.Light))
                    {
                        grid.UpdateCell(Rows - 1 - cell.X, cell.Y, CellState.Full);
                        Array.Find(tempCells, c => c.X == cell.X && c.Y == cell.Y).State = CellState.Full;
                    }

                }

                // Apply the rule for live cells and update array
                for (int i = 0; i < Survivals.Count; i++)
                {
                    if (countLive == Survivals[i] && cell.State == CellState.Full)
                    {
                        grid.UpdateCell(Rows - 1 - cell.X, cell.Y, CellState.Full);
                        Array.Find(tempCells, c => c.X == cell.X && c.Y == cell.Y).State = CellState.Full;
                    }
                }
            }
  
            int countCell = 0;
            int countBlank = 0;

            for (int i = 0; i < LifeCells.Length; i++)
            {
                if (tempCells[i].State != LifeCells[i].State)
                {
                    countCell++;
                } 
                else if (tempCells[i].State == CellState.Blank)
                {
                    countBlank++;
                }
            }

            if(countCell == 0)
            {
                countGen++;
                if (countBlank == LifeCells.Length)
                {
                    nonPeriodic = true;
                }
            }

            // Copy the tempurary array to the actual lifecells array for the next generation
            for (int i = 0; i < tempCells.Length; i++)
                LifeCells[i] = (BaseLifeCell)tempCells[i].Clone();
        }
    }
}
