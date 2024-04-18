namespace CanyonDuty
{
    public partial class Form1 : Form
    {
        Canvas canvas;
        List<VPoint> balls;
        VRope rope;
        List<VBox> boxes;
        VSolver solver;
        Point mouse, trigger;
        private VTank tank1;
        private VTank tank2;
        private bool isMouseDown = false;
        private VTank draggedTank = null;
        private VObstacle obstacle1, obstacle2;
        Point currentMousePosition;
        Bitmap tanque1, tanque2;
        int motion1 = 2;
        int motion2 = 6;
        int width = 688;
        Bitmap layer0, layer1, layer2, layer00;
        int l0_X1, l0_X2, l0_X3, l0_X4, l1_X1, l1_X2, l2_X1;
        static Graphics g;
        List<VObstacle> obstacles;

        public Form1()
        {
            InitializeComponent();
            layer0 = recursos.nube;
            layer1 = recursos.ave;
            layer2 = recursos.jetmilitar;
            l0_X1 = l1_X1 = 0;
            l0_X2 = l1_X2 = l0_X3 = l0_X4 = l2_X1 = width;
            // Initialize other objects
            //Recursos
            tanque1 = recursos.tanqueizq;
            tanque2 = recursos.tanqueder;
            int nuevoAncho = 70;
            int nuevoAlto = 70;
            tanque1 = new Bitmap(tanque1, new Size(nuevoAncho, nuevoAlto));
            tanque2 = new Bitmap(tanque2, new Size(nuevoAncho, nuevoAlto));


            // Initialize the tanks
            tank1 = new VTank(tanque1, 300, 400, 20, 20, 1);
            tank2 = new VTank(tanque2, 1600, 700, 20, 20, 2);
            //Init();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void Init()
        {
            canvas = new Canvas(PCT_CANVAS.Size);
            PCT_CANVAS.Image = canvas.bmp;
            balls = new List<VPoint>();
            boxes = new List<VBox>();

            // Dentro del método Init() o en cualquier otro lugar apropiado
            PCT_CANVAS.BackgroundImage = recursos.fondo;
            PCT_CANVAS.BackgroundImageLayout = ImageLayout.Stretch - 2; // O el layout que desees


            int halfWidth = PCT_CANVAS.Width / 2;
            int tankWidth = 30; // The width of the tank
            int tankHeight = 30; // The height of the tank
            int tank1X = halfWidth / 2 - tankWidth / 2;
            int tank2X = halfWidth + halfWidth / 2 - tankWidth / 2;
            int tankY = PCT_CANVAS.Height / 2 - tankHeight / 2; // The y position of the tanks

            // Create tank1 using the tanque1 image
            VTank tank1 = new VTank(tanque1, 100, 100, 5, 5, 1);
            // Create tank2 using the tanque2 image
            VTank tank2 = new VTank(tanque2, 300, 200, 5, 5, 2);

            boxes.Add(tank1);
            boxes.Add(tank2);

            obstacle1 = new VObstacle(PCT_CANVAS.Size, 15, 40, 40, 30, -5);
            for (int i = 0; i < obstacle1.grid.Count; i++)
            {
                balls.AddRange(obstacle1.grid[i]);
            }
            obstacle2 = new VObstacle(PCT_CANVAS.Size, 15, 40, -30, 150, 5);
            for (int i = 0; i < obstacle2.grid.Count; i++)
            {
                balls.AddRange(obstacle2.grid[i]);
            }
            solver = new VSolver(balls);

        }


        private void PCT_CANVAS_Paint(object sender, PaintEventArgs e)
        {
 
            canvas.FastClear();


            // Render the tanks
            tank1.Render(canvas.g, PCT_CANVAS.Width, PCT_CANVAS.Height);
            tank2.Render(canvas.g, PCT_CANVAS.Width, PCT_CANVAS.Height);

            // Render the obstacle


            // If the mouse is down and a tank is being dragged
            if (isMouseDown && draggedTank != null)
            {
                // Draw a line from the shot start position to the current mouse position
                // Use a new Pen object with a thickness of 3
                canvas.g.DrawLine(new Pen(Color.Red, 3), draggedTank.ShotStartPosition.X, draggedTank.ShotStartPosition.Y, currentMousePosition.X, currentMousePosition.Y);
            }
            solver.Update(canvas.g, canvas.Width, canvas.Height);
            obstacle1.Render(canvas.g, PCT_CANVAS.Width, PCT_CANVAS.Height);
            obstacle2.Render(canvas.g, PCT_CANVAS.Width, PCT_CANVAS.Height);

            // parallax
            g = e.Graphics;

            // Redimensionar las imágenes
            int nuevoAncho = 180; // Nuevo ancho más angosto
            int nuevoAlto = 180; // Nuevo alto más corto

            Bitmap resizedLayer0 = new Bitmap(layer0, new Size(nuevoAncho, nuevoAlto));
            Bitmap resizedLayer1 = new Bitmap(layer1, new Size(nuevoAncho, nuevoAlto));
            Bitmap resizedLayer2 = new Bitmap(layer2, new Size(nuevoAncho, nuevoAlto));

            // Coordenadas de dibujo para las imágenes más a la derecha
            int offsetX = PCT_CANVAS.Width / 2; // Desplazamiento hacia la derecha
            int offsetY = 0; // Ajusta la posición vertical según sea necesario

            // Dibujar las imágenes redimensionadas
            g.DrawImage(resizedLayer0, l0_X1 + offsetX, offsetY, resizedLayer0.Width + 20, resizedLayer0.Height + 20);
            g.DrawImage(resizedLayer0, l0_X2 + offsetX, 200, resizedLayer0.Width - 30, resizedLayer0.Height - 30);
            g.DrawImage(resizedLayer0, l0_X3 + -380, 500, resizedLayer0.Width - 10, resizedLayer0.Height - 10);
            g.DrawImage(resizedLayer0, l0_X4 + offsetX, 800, resizedLayer0.Width - 50, resizedLayer0.Height - 50);

            g.DrawImage(resizedLayer1, l1_X1 + offsetX, offsetY, resizedLayer1.Width - 50, resizedLayer1.Height - 50);
            g.DrawImage(resizedLayer1, l1_X2 + -400, 400, resizedLayer1.Width - 50, resizedLayer1.Height - 50);

            g.DrawImage(resizedLayer2, l2_X1 + offsetX, 100, 110, 110);

            // Liberar recursos
            resizedLayer0.Dispose();
            resizedLayer1.Dispose();
            resizedLayer2.Dispose();

        }

        private void PCT_CANVAS_MouseClick(object sender, MouseEventArgs e)
        {
            // Get the position of the mouse click
            Point clickPosition = e.Location;
        }

        private float CalculateAngle(Vec2 tankPosition, Point clickPosition)
        {
            float dx = clickPosition.X - tankPosition.X;
            float dy = clickPosition.Y - tankPosition.Y;

            // Calculate the angle in radians
            float angle = (float)Math.Atan2(dy, dx);

            // Convert the angle to degrees
            angle = angle * 180 / (float)Math.PI;

            return angle;
        }

        private void TIMER_Tick(object sender, EventArgs e)
        {
            BackgroundMove();
            if (solver.tankCollision(tank1))
            {
                // Actualiza el texto de Label3 con la vida de tank1
                label1.Text = "Vidas: " + tank1.life;
            }
            if (solver.tankCollision(tank2))
            {
                // Actualiza el texto de Label1 con la vida de tank2
                label3.Text = "Vidas: " + tank2.life;
            }

            // Verifica si la vida de alguno de los tanques ha llegado a 0
            if (tank1.life <= 0)
            {
                MessageBox.Show("El tanque 2 ha ganado!");
                Application.Exit(); // Cierra la aplicación

            }
            else if (tank2.life <= 0)
            {
                MessageBox.Show("El tanque 1 ha ganado!");
                Application.Exit(); // Cierra la aplicación
                                    // O puedes llamar a Init() para reiniciar el juego
            }

            PCT_CANVAS.Invalidate();
        }



        private void PCT_CANVAS_MouseMove(object sender, MouseEventArgs e)
        {
            // If the mouse is down and a tank is being dragged
            if (isMouseDown && draggedTank != null)
            {
                // Update the current mouse position
                currentMousePosition = e.Location;
            }

            // Redraw the PictureBox
            PCT_CANVAS.Invalidate();
        }

        private void PCT_CANVAS_MouseDown(object sender, MouseEventArgs e)
        {
            // Get the position of the mouse click
            Point clickPosition = e.Location;

            // Check if any tank is under the mouse
            if (tank1.IsUnder(clickPosition))
            {
                isMouseDown = true;
                draggedTank = tank1;
                tank1.ShotStartPosition = new Vec2(clickPosition.X, clickPosition.Y);
            }
            else if (tank2.IsUnder(clickPosition))
            {
                isMouseDown = true;
                draggedTank = tank2;
                tank2.ShotStartPosition = new Vec2(clickPosition.X, clickPosition.Y);
            }
        }

        private void PCT_CANVAS_MouseUp(object sender, MouseEventArgs e)
        {
            // If the mouse is down and a tank is being dragged
            if (isMouseDown && draggedTank != null)
            {
                // Calculate the shot power based on the distance between the start position and the mouse position
                draggedTank.ShotPower = (float)Math.Sqrt(Math.Pow(draggedTank.ShotStartPosition.X - currentMousePosition.X, 2) +
                                                         Math.Pow(draggedTank.ShotStartPosition.Y - currentMousePosition.Y, 2));

                // Calculate the shot angle
                float dx = currentMousePosition.X - draggedTank.ShotStartPosition.X;
                float dy = currentMousePosition.Y - draggedTank.ShotStartPosition.Y;
                draggedTank.CannonAngle = ((float)Math.Atan2(dy, dx) * 180 / (float)Math.PI) + 180; // Add 180 degrees to invert the angle

                // Make the dragged tank shoot
                draggedTank.Shoot();
                balls.Add(draggedTank.Ball);

                // Reset the mouse down and dragged tank
                isMouseDown = false;
                draggedTank = null;
            }
        }

        private void PCT_CANVAS_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void BackgroundMove()
        {
            // Obtener el ancho de la pantalla
            int screenWidth = this.Width;

            // Mover las imágenes
            l0_X1 -= -motion1;
            l0_X2 -= 4;
            l0_X3 -= -3;
            l0_X4 -= 8;
            l1_X1 -= motion2;
            l1_X2 -= -motion2;
            l2_X1 -= 15;

            // Verificar si la imagen llega al final de la pantalla
            if (l0_X1 + screenWidth <= 0)
            {
                // Si la imagen se desplaza completamente fuera de la pantalla, reiniciar su posición
                l0_X1 = screenWidth;
            }
            if (l0_X2 + screenWidth <= 0)
            {
                l0_X2 = screenWidth;
            }
            if (l0_X3 + screenWidth <= 0)
            {
                l0_X3 = screenWidth;
            }
            if (l0_X4 + screenWidth <= 0)
            {
                l0_X4 = screenWidth;
            }
            if (l1_X1 + screenWidth <= 0)
            {
                l1_X1 = screenWidth;
            }
            if (l1_X2 + screenWidth <= 0)
            {
                l1_X2 = screenWidth;
            }
            if (l2_X1 + screenWidth <= 0)
            {
                l2_X1 = screenWidth;
            }

            // Invalidar el área del control para forzar un repintado
            Invalidate();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Velocidad de movimiento de los tanques
            int speed = 5;

            // Tamaño del tanque
            int tankWidth = 70; // Asegúrate de que este valor sea el correcto
            int tankHeight = 70; // Asegúrate de que este valor sea el correcto

            switch (e.KeyCode)
            {
                case Keys.A:
                    // Mover el tanque1 a la izquierda si no está en el borde izquierdo
                    if (tank1.Position.X - speed >= tankWidth / 2)
                        tank1.Position.X -= speed;
                    break;
                case Keys.D:
                    // Mover el tanque1 a la derecha si no está en el borde derecho
                    if (tank1.Position.X + speed <= PCT_CANVAS.Width - tankWidth / 2)
                        tank1.Position.X += speed;
                    break;
                case Keys.W:
                    // Mover el tanque1 hacia arriba si no está en el borde superior
                    if (tank1.Position.Y - speed >= tankHeight / 2)
                        tank1.Position.Y -= speed;
                    break;
                case Keys.S:
                    // Mover el tanque1 hacia abajo si no está en el borde inferior
                    if (tank1.Position.Y + speed <= PCT_CANVAS.Height - tankHeight / 2)
                        tank1.Position.Y += speed;
                    break;
                case Keys.Left:
                    // Mover el tanque2 a la izquierda si no está en el borde izquierdo
                    if (tank2.Position.X - speed >= tankWidth / 2)
                        tank2.Position.X -= speed;
                    break;
                case Keys.Right:
                    // Mover el tanque2 a la derecha si no está en el borde derecho
                    if (tank2.Position.X + speed <= PCT_CANVAS.Width - tankWidth / 2)
                        tank2.Position.X += speed;
                    break;
                case Keys.Up:
                    // Mover el tanque2 hacia arriba si no está en el borde superior
                    if (tank2.Position.Y - speed >= tankHeight / 2)
                        tank2.Position.Y -= speed;
                    break;
                case Keys.Down:
                    // Mover el tanque2 hacia abajo si no está en el borde inferior
                    if (tank2.Position.Y + speed <= PCT_CANVAS.Height - tankHeight / 2)
                        tank2.Position.Y += speed;
                    break;
            }

            // Redibujar el PictureBox
            PCT_CANVAS.Invalidate();
        }


    }
}
