using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace CanyonDuty.MapaChars
{
    public class Brick
    {

        public static void DrawGreenBrick(Graphics grx, int x, int y, int unit)
        {
            grx.FillRectangle(Brushes.Green, x * unit, y * unit, unit, unit);
            grx.FillRectangle(Brushes.DarkCyan, x * unit + 4, y * unit + 4, unit - 8, unit - 8);

            grx.DrawLine(Pens.DarkGray, x * unit, y * unit, x * unit + unit, y * unit + unit - 1);

            grx.DrawLine(Pens.DimGray, x * unit, y * unit, x * unit + unit / 2, y * unit + unit / 2);
            grx.DrawLine(Pens.DarkGray, x * unit, y * unit + unit, x * unit + unit, y * unit);
        }
    }
}
