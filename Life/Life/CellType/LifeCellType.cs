using System;
using System.Collections.Generic;
using System.Text;

namespace Life.CellType
{
    public class LifeCellType : BaseLifeCellType
    {
        public LifeCellType(int[] coords) : base(coords)
        {
            LifeCellCoords.Add(new LifeCellCoord
            {
                X = Coords[0],
                Y = Coords[1]
            });
        }
    }
}
