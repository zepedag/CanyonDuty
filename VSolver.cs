using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Reflection;

namespace CanyonDuty
{
    public class VSolver
    {
        VPoint p1, p2;
        Vec2 axis, normal, res;
        float dis, dif;
        internal List<VPoint> pts;
        public VSolver(List<VPoint> pts)
        {
            this.pts = pts;
        }

        public int Update(Graphics g, int Width, int Height)
        {
            int id;

            id = -1;

            for (int s = 0; s < pts.Count; s++)
            {
                for (int p = s; p < pts.Count; p++)
                {
                    p1 = pts[s];
                    p2 = pts[p];

                    if (p1.Id == p2.Id)// BY ID
                        continue;

                    if (p1.IsPinned && p2.IsPinned)
                        continue;
                    if (p1.isInsideGrid && p2.isInsideGrid)
                        continue;

                    axis = p1.Pos - p2.Pos;//vector de direccion
                    dis = axis.Length(); // magnitud

                    if (dis < p1.Radius + p2.Radius)//COLLISION DETECTED
                    {
                        if(p1.isWall)
                            p1.IsPinned = false;
                        if (p2.isWall)
                            p2.IsPinned = false;
                        dif = (dis - (p1.Radius + p2.Radius)) * .5f;// dividir la fuerza para repatar entre ambas colisiones
                        normal = axis / dis; // normalizar la direccion para tener el vector unitario
                        res = dif * normal;// vector resultante

                        if (!p1.IsPinned)
                            if (p2.IsPinned)
                                p1.Pos -= res * 2;
                            else
                                p1.Pos -= res;

                        if (!p2.IsPinned)
                            if (p1.IsPinned)
                                p2.Pos += res * 2;
                            else
                                p2.Pos += res;
                    }
                }
                /*
                if (isMouseDown)// para seleccionar el punto de masa a mover escogiendo su ID 
                    if (Math.Abs((p1.X - mouse.X) * (p1.X - mouse.X) + (p1.Y - mouse.Y) * (p1.Y - mouse.Y)) <= p1.Radius * p1.Radius)
                        id = p1.Id;*/
                if(p1.isActive && !p1.isWall && !p1.isBullet)
                    p1.Render(g, Width, Height);
            }
            
            


            return id;
        }


        public bool tankCollision(VTank tank)
        {
            int hitboxSize = 50; // Aumenta este valor para hacer el hitbox más grande

            for (int i = 0; i < pts.Count; i++)
            {
                if (!pts[i].isBullet) // Si el objeto no es una bala, ignóralo
                    continue;

                // Comprueba si la bala está dentro del cuadro delimitador del tanque
                if (pts[i].X >= tank.Position.X - hitboxSize && pts[i].X <= tank.Position.X + 30 + hitboxSize &&
                    pts[i].Y >= tank.Position.Y - hitboxSize && pts[i].Y <= tank.Position.Y + 30 + hitboxSize)
                {
                    tank.life--; // Reduce la vida del tanque
                    return true;
                }
            }

            return false;
        }






    }
}
