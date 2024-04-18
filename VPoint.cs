using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace CanyonDuty
{
    public class VPoint
    {
        bool isPinned = false;
        bool fromBody = false;
        Vec2 pos, old, vel, gravity;
        internal int id;
        public float Mass;
        float radius, bounce, diameter, m, frict = 0.6f;
        float groundFriction = 0.7f;
        float bounceDamping = 1.1f; // Factor de amortiguación para los rebotes
        int bounceCount = 0; // Contador de rebotes
        internal bool isWall, isInsideGrid;
        public Color c;
        public Brush brush;
        internal bool isActive;
        internal bool isBullet;

        public void Move()
        {
            X += vel.X;
            Y += vel.Y;
        }

        public bool FromBody
        {
            get { return fromBody; }
            set { fromBody = value; }
        }
        public float Diameter
        {
            get { return diameter; }
            set { diameter = value; }
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public bool IsPinned
        {
            get { return isPinned; }
            set { isPinned = value; }
        }
        public float X
        {
            get { return pos.X; }
            set { pos.X = value; }
        }
        public float Y
        {
            get { return pos.Y; }
            set { pos.Y = value; }
        }
        public Vec2 Pos
        {
            get { return pos; }
            set { pos = value; }
        }
        public Vec2 Old
        {
            get { return old; }
            set { old = value; }
        }
        public float Radius
        {
            get { return radius; }
            set { radius = value; diameter = radius + radius; }
        }

        public VPoint(int x, int y)
        {
            id = -1;
            Init(x, y, 0, 0);
        }
        public VPoint(int x, int y, int id, bool Pinned)
        {
            this.id = id;
            isPinned = Pinned;
            Init(x, y, 0, 0);
        }
        public VPoint(int x, int y, int id)
        {
            this.id = id;
            Init(x, y, 0, 0);
        }
        public VPoint(int x, int y, float vx, float vy, int id, bool Pinned)
        {
            this.id = id;
            isPinned = Pinned;
            Init(x, y, vx, vy);
        }

        public VPoint(int x, int y, float vx, float vy, int id)
        {
            this.id = id;
            Init(x, y, vx, vy);
        }

        private void Init(int x, int y, float vx, float vy)
        {
            pos = new Vec2(x, y);
            old = new Vec2(x, y);
            gravity = new Vec2(0, 5);//
            vel = new Vec2(vx, vy);
            radius = 15;
            diameter = radius + radius;
            Mass = 1f;
            bounce = 5f;
            c = Color.LightPink;
            brush = new SolidBrush(c);
            isWall = false;
            isInsideGrid = false;
            isActive = true;
            isBullet = false;
            if (IsPinned)
            {
                Pin();
            }
        }

        public void Pin()
        {
            brush = new SolidBrush(Color.MediumPurple);
            radius = 20;
            diameter = radius + radius;
            isPinned = true;
        }

        public void Update(int width, int height)
        {

            if (isPinned)
                return;

            if (bounceCount < 5) // Si la bola ha rebotado menos de 5 veces, aplicar el factor de amortiguación
            {
                vel = (pos - old) * frict * bounceDamping;
            }
            else // Si la bola ha rebotado 5 veces, no aplicar el factor de amortiguación
            {
                vel = (pos - old) * frict;
            }

            old = pos;
            pos += vel + gravity;
        }

        public void Constraints(int width, int height)
        {
            if (bounceCount >= 5) // Si la bola ha rebotado 5 veces, detenerla completamente
            {
                vel.X = 0;
                vel.Y = 0;
                return;
            }

            if (pos.X > width - radius)
            {
                pos.X = width - radius;
                vel.X = -vel.X; // Invertir la velocidad en la dirección x
                bounceCount++; // Incrementar el contador de rebotes
            }
            if (pos.X < radius)
            {
                pos.X = radius;
                vel.X = -vel.X; // Invertir la velocidad en la dirección x
                bounceCount++; // Incrementar el contador de rebotes
            }
            if (pos.Y > height - radius)
            {
                pos.Y = height - radius;
                vel.Y = -vel.Y; // Invertir la velocidad en la dirección y
                bounceCount++; // Incrementar el contador de rebotes
            }
            if (pos.Y < radius)
            {
                pos.Y = radius;
                vel.Y = -vel.Y; // Invertir la velocidad en la dirección y
                bounceCount++; // Incrementar el contador de rebotes
            }
        }
        public void RenderAsBrick(Graphics g, int width, int height)
        {
            if (!isActive)
                return;

            Update(width, height);
            Constraints(width, height);

            Color brickColor = Color.Red; 
            Color borderColor = Color.DarkRed; 
            float borderWidth = 2f; 

            using (SolidBrush brickBrush = new SolidBrush(brickColor))
            using (Pen borderPen = new Pen(borderColor, borderWidth))
            {
                float squareSide = radius * 2;
                float squareX = pos.X - radius;
                float squareY = pos.Y - radius;

                g.FillRectangle(brickBrush, squareX, squareY, squareSide, squareSide);

                g.DrawRectangle(borderPen, squareX, squareY, squareSide, squareSide);

                
                using (Pen linePen = new Pen(borderColor, 1f)) 
                {
                    float lineY = squareY + squareSide / 2; 
                    g.DrawLine(linePen, squareX, lineY, squareX + squareSide, lineY);
                }
            }
        }

        public void Render(Graphics g, int width, int height)
        {
            if (fromBody)
                return;

            Update(width, height);
            Constraints(width, height);

            g.FillEllipse(brush, pos.X - radius, pos.Y - radius, diameter, diameter);
        }


        public override string ToString()
        {
            return "ID: " + id + " : " + pos.ToString();
        }

    }
}


