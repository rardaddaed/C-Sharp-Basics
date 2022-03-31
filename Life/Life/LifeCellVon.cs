using Display;
using System;
using System.Collections.Generic;
using System.Text;

namespace Life
{
    public class LifeCellVon : BaseLifeCell
    {
        public LifeCellVon(int x, int y, int order) : base(x, y, order) { }
        public LifeCellVon(int x, int y, CellState state, int order) : base(x, y, state, order) { }

        public override object Clone()
        {
            return new LifeCellVon(X, Y, State, Order);
        }

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
                    // Determine the Manhattan Distance
                    if ((Math.Abs(k - X) + Math.Abs(l - Y)) <= Order)
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
            }
            return countLive;
        }

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
                    if ((Math.Abs(neighbourX - X) + Math.Abs(neighbourY - Y)) <= Order)
                    {
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
            }
            return countLive;
        }
    }
}
