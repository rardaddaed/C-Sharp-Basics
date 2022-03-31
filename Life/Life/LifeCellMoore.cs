using Display;
using System;
using System.Collections.Generic;
using System.Text;

namespace Life
{
    /// <summary>
    /// Class for every cell, also counts the live neighbours.
    /// Using ICloneable to duplicate array of objects (from temp to lifecells)
    /// </summary>
    /// 
    public class LifeCellMoore : BaseLifeCell
    {
        /// <summary>
        /// Contructor to create a cell
        /// </summary>
        public LifeCellMoore(int x, int y, int order) : base(x, y, order) { }

        /// <summary>
        /// Constructor to create a cell with specified state
        /// </summary>
        public LifeCellMoore(int x, int y, CellState state, int order) : base(x, y, state, order) { }

        /// <summary>
        /// To clone objects
        /// </summary>
        /// <returns>An object of cell</returns>
        public override object Clone()
        {
            return new LifeCellMoore(X, Y, State, Order);
        }

        /// <summary>
        /// Method to count the live neighbour cells for a cell
        /// </summary>
        /// <returns>The number of live neighbours</returns>
        public override int CountLiveNeighbours(BaseLifeCell[] lifeCells, int rows, int cols, bool centreCount)
        {
            // Set the boundaries for neighbours
            int countLive = 0;
            int minNeiX = X < Order ? 0 : X - Order;
            int minNeiY = Y < Order ? 0 : Y - Order;
            int maxNeiX = X + Order > rows - 1 ? rows - 1 : X + Order;
            int maxNeiY = Y + Order > cols - 1 ? cols - 1 : Y + Order;

            // Count the live neighbours
            for (int k = minNeiX; k <= maxNeiX; k++)
            {
                for (int l = minNeiY; l <= maxNeiY; l++)
                {
                    if (centreCount)
                    {
                        if (Array.Find(lifeCells, c => c.X == k && c.Y == l).State == CellState.Full)
                        {
                            countLive++;
                        }
                    }
                    else
                    {
                        if (!(X == k && Y == l))
                        {
                            if (Array.Find(lifeCells, c => c.X == k && c.Y == l).State == CellState.Full)
                            {
                                countLive++;
                            }
                        }
                    }
                }
            } 
              return countLive;
        }

        /// <summary>
        /// Method to count the live neighbour cells for a cell in periodic mode
        /// </summary>
        /// <returns>The number of live neighbours</returns>
        public override int CountLiveNeighboursPeriodic(BaseLifeCell[] lifeCells, int rows, int cols, bool centreCount)
        {
            int countLive = 0;

            // Count the live neighbour cells with periodic rules applied, using modulus operator % for boundary conditions
            for (int k = X - Order; k <= X + Order; k++)
            {
                for (int l = Y - Order; l <= Y + Order; l++)
                {
                    int neighbourX = (k + rows) % rows;
                    int neighbourY = (l + cols) % cols;
                    if (centreCount)
                    {
                        if (Array.Find(lifeCells, c => c.X == neighbourX && c.Y == neighbourY).State == CellState.Full)
                        {
                            countLive++;
                        }
                    }
                    else
                    {
                        if (!(X == neighbourX && Y == neighbourY))
                        {
                            if (Array.Find(lifeCells, c => c.X == neighbourX && c.Y == neighbourY).State == CellState.Full)
                            {
                                countLive++;
                            }
                        }
                    }
                }
            }
            return countLive;
        }
    }
}
