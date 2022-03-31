using System;
using System.Collections.Generic;
using System.Text;

namespace Life.CellType
{
    public class LifeCellTypeEllipse : BaseLifeCellType
    {
        public LifeCellTypeEllipse(int[] coords): base(coords)
        {
            int width = Coords[3] - Coords[1];
            int height = Coords[2] - Coords[0];
            double centreX = (double)width / 2 + Coords[1];
            double centreY = (double)height / 2 + Coords[0];
            for (int i = Coords[1]; i < Coords[3]; i++)
            {
                for (int j = Coords[0]; j < Coords[2]; j++)
                {
                    double result = (4 * Math.Pow((i - centreX), 2) / Math.Pow(width, 2)) + (4 * Math.Pow((j - centreY), 2) / Math.Pow(height, 2));
                    if (result <= 1)
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
}
