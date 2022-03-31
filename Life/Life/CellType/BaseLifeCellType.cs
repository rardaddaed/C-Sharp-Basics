using System;
using System.Collections.Generic;
using System.Text;

namespace Life.CellType
{

    public class BaseLifeCellType
    {
        public int[] Coords { get; set; }
        public List<LifeCellCoord> LifeCellCoords { get; set; } = new List<LifeCellCoord>();
        public BaseLifeCellType(int[] coords)
        {
            Coords = (int[])coords.Clone();
        }
    }

    public class LifeCellCoord
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}
