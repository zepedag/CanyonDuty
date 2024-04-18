using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace CanyonDuty
{
    public class VTank : VBox
    {
        public float CannonAngle { get; set; }
        public Bitmap Image { get; set; }
        public float ShotPower { get; set; }
        public VPoint Ball { get; set; }
        public Vec2 ShotStartPosition { get; set; }
        public int life {  get; set; }
        public Color TankColor { get; set; }
        public VTank(Bitmap image,int x, int y, int width, int height, int id) : base(x, y, width, height, id)
        {
            CannonAngle = 0;
            ShotPower = 10f;
            life = 4;
            Image = image;
            TankColor = id == 1 ? Color.Transparent : Color.Transparent;
            brush = new SolidBrush(TankColor);
            for (int i = 0; i < boxPoints.Count; i++)
            {
                boxPoints[i].IsPinned = true;
            }
            TankColor = id == 1 ? Color.Transparent : Color.Transparent;
            brush = new SolidBrush(TankColor);
        }

        public void Shoot()
        {
            float radianAngle = CannonAngle * (float)Math.PI / 180;
            float vx = ShotPower * (float)Math.Cos(radianAngle);
            float vy = ShotPower * (float)Math.Sin(radianAngle);

            float initialX = Position.X + (15 / 2 + 35) * (float)Math.Cos(radianAngle);
            float initialY = Position.Y + (15 / 2 + 35) * (float)Math.Sin(radianAngle);

            Ball = new VPoint((int)initialX, (int)initialY, vx, vy, BoxId);
            Ball.isBullet = true;
        }

        public override void Render(Graphics g, int width, int height)
        {
            base.Render(g, width, height);

            if (Image != null)
            {
                // Calcula el tamaño deseado de la imagen (ajusta según sea necesario)
                int nuevoAncho = 70;
                int nuevoAlto = 70;

                // Calcula la posición de dibujo del tanque (ajusta según sea necesario)
                int x = (int)Position.X - nuevoAncho / 2;
                int y = (int)Position.Y - nuevoAlto / 2;

                // Dibuja la imagen del tanque en el lienzo con el tamaño ajustado
                g.DrawImage(Image, new Rectangle(x, y, nuevoAncho, nuevoAlto));
            }

            if (Ball != null)
            {
                Ball.Move();
                Ball.Update(width, height);
                Ball.brush = new HatchBrush(HatchStyle.DiagonalCross, Color.White, Color.Black);
                Ball.Render(g, width, height);
            }
        }

        public override void Update(int width, int height)
        {
            // No actualizamos los puntos de masa a, b, c, d para que no caigan los tanques

            // Pero sí actualizamos los polos y los puntos para el dibujo
            p1.Update(); p2.Update(); p3.Update(); p4.Update(); p5.Update(); p6.Update();

            pts[0] = new PointF(a.Pos.X, a.Pos.Y);
            pts[1] = new PointF(b.Pos.X, b.Pos.Y);
            pts[2] = new PointF(c.Pos.X, c.Pos.Y);
            pts[3] = new PointF(d.Pos.X, d.Pos.Y);

            BoundingBox();
        }

        public bool IsUnder(Point point)
        {
            // Calcula la distancia entre el centro del tanque y el punto
            float dx = Position.X - point.X;
            float dy = Position.Y - point.Y;
            float distance = (float)Math.Sqrt(dx * dx + dy * dy);

            // Si la distancia es menor que el radio del tanque, entonces el punto está dentro del tanque
            return distance <= 25 / 2;
        }


    }
}
