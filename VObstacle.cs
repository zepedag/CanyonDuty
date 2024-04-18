using CanyonDuty;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CanyonDuty
{
    public class VObstacle
    {
        public List<List<VPoint>> grid;
        private List<VPole> poles;
        private int canvasWidth, canvasHeight;
        private int gridWidth,gridHeight;
        private int spacingX, spacingY;
        private int radius;
        private int offsetX;
        private int offsetY;
        public float Speed { get; set; }
        public VObstacle(Size size, int wallWidth, int wallHeight, int offsetX, int offsetY, float Speed)
        {
            gridWidth = wallWidth;
            gridHeight = wallHeight;
            canvasHeight = size.Height;
            canvasWidth = size.Width;
            spacingX = 5;
            spacingY = 5;
            this.Speed = Speed;
            radius = 15;
            this.offsetX = offsetX;
            this.offsetY = offsetY;
            grid = new List<List<VPoint>>();
            poles = new List<VPole>();
            GenerateWallPoints();
        }
       
        public void GenerateWallPoints()
        {
            int halfWidth = canvasWidth / 2;
            int halfHeight = canvasHeight / 2;
            int obstacleWidth = gridWidth * spacingX;
            int halfObstacleWidth = obstacleWidth / 2;

            for (int i = 0; i < gridHeight; i+= spacingY)
            {
                List<VPoint> row = new List<VPoint>();
                for (int j = 0; j < gridWidth; j+=spacingX)
                {
                    VPoint point = new VPoint((j + offsetX) * spacingX + halfWidth - halfObstacleWidth, (i + offsetY) * spacingY , id: j + i * gridWidth);
                    point.IsPinned = true;
                    point.isWall = true;
                    point.isInsideGrid = true;
                    row.Add(point);
                }
                grid.Add(row);
            }
        }
       

        public void Render(Graphics g,int width, int height)
        {
            
            UpdatePosition();

            for (int i = 0;  i< grid.Count; i++)
            {
    
                for (int j = 0; j < grid[i].Count; j++)
                {
                    grid[i][j].RenderAsBrick(g, width, height);
                }
            }
        }



        public void UpdatePosition()
        {
            float highestY = float.MaxValue;
            float lowestY = float.MinValue;
            bool foundPinnedPoint = false; 

            foreach (var row in grid)
            {
                foreach (var point in row)
                {
                    if (!point.IsPinned) continue; 

                    foundPinnedPoint = true; 
                    highestY = Math.Min(highestY, point.Y);
                    lowestY = Math.Max(lowestY, point.Y);
                }
            }

            if (!foundPinnedPoint) return;

            if (highestY + Speed - radius <= 0 || lowestY + Speed + radius >= canvasHeight - 1)
            {
                Speed = -Speed; 
            }

            
            foreach (var row in grid)
            {
                foreach (var point in row)
                {
                    
                    if (!point.IsPinned) continue; 
                    point.Y += Speed;
                }
            }
        }



    }
}




