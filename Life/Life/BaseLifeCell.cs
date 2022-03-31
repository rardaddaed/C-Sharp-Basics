using Display;
using System;
using System.Collections.Generic;
using System.Text;

namespace Life
{
    public abstract class BaseLifeCell : ICloneable
    {
        public int X { get; }
        public int Y { get; }
        public CellState State { get; set; }
        public int Order { get; }

        /// <summary>
        /// Contructor to create a cell
        /// </summary>
        public BaseLifeCell(int x, int y, int order)
        {
            State = CellState.Blank;
            X = x;
            Y = y;
            Order = order;
        }

        /// <summary>
        /// Constructor to create a cell with specified state
        /// </summary>
        public BaseLifeCell(int x, int y, CellState state, int order)
        {
            X = x;
            Y = y;
            State = state;
            Order = order;
        }

        public CellState ChangeToDead(bool ghost)
        {
            if (ghost)
            {
                switch (State)
                {
                    case CellState.Full:
                        State = CellState.Dark;
                        break;
                    case CellState.Dark:
                        State = CellState.Medium;
                        break;
                    case CellState.Medium:
                        State = CellState.Light;
                        break;
                    case CellState.Light:
                        State = CellState.Blank;
                        break;
                }
            }
            else
            {
                State = CellState.Blank;
            }
            return State;
        }

        public abstract object Clone();
        public abstract int CountLiveNeighbours(BaseLifeCell[] lifeCells, int rows, int cols, bool centreCount);

        public abstract int CountLiveNeighboursPeriodic(BaseLifeCell[] lifeCells, int rows, int cols, bool centreCount);


    }
}
