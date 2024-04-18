using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace CanyonDuty.MapaChars
{
    public class Camera
    {
        public PointF Pos;
        public PointF Vel;

        public Camera()
        {
            Pos = new PointF(0, 0);
            Vel = new PointF(0, 0);
        }

        public void Update(float elapsed)
        {
            Pos.X += Vel.X * elapsed;
            Pos.Y += Vel.Y * elapsed;
        }

    }
}
