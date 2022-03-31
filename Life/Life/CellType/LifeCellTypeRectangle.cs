using System;
using System.Collections.Generic;
using System.Text;

namespace Life.CellType
{
    public class LifeCellTypeRectangle : BaseLifeCellType
    {
        public LifeCellTypeRectangle(int[] coords) : base(coords)
        {
            for (int i = Coords[1]; i < Coords[3]; i++)
            {
                for (int j = Coords[0]; j < Coords[2]; j++)
                {
                    LifeCellCoords.Add(new LifeCellCoord
                    {
                        X = i,
                        Y = j
                    });
                }
            }
        }
    }
}
