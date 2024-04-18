namespace CanyonDuty
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            TIMER = new System.Windows.Forms.Timer(components);
            label2 = new Label();
            label3 = new Label();
            label1 = new Label();
            PCT_CANVAS = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)PCT_CANVAS).BeginInit();
            SuspendLayout();
            // 
            // TIMER
            // 
            TIMER.Enabled = true;
            TIMER.Interval = 30;
            TIMER.Tick += TIMER_Tick;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(380, 12);
            label2.Name = "label2";
            label2.Size = new Size(0, 20);
            label2.TabIndex = 2;
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Dock = DockStyle.Right;
            label3.Font = new Font("Tempus Sans ITC", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = SystemColors.HotTrack;
            label3.Location = new Point(724, 0);
            label3.Name = "label3";
            label3.Size = new Size(76, 26);
            label3.TabIndex = 4;
            label3.Text = "Vidas: 5";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.Window;
            label1.Cursor = Cursors.SizeWE;
            label1.Dock = DockStyle.Left;
            label1.Font = new Font("Tempus Sans ITC", 12F);
            label1.ForeColor = Color.Red;
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(76, 26);
            label1.TabIndex = 5;
            label1.Text = "Vidas: 5";
            label1.Click += label1_Click;
            // 
            // PCT_CANVAS
            // 
            PCT_CANVAS.BackColor = Color.FromArgb(0, 64, 0);
            PCT_CANVAS.Dock = DockStyle.Fill;
            PCT_CANVAS.Location = new Point(0, 0);
            PCT_CANVAS.Name = "PCT_CANVAS";
            PCT_CANVAS.Size = new Size(800, 451);
            PCT_CANVAS.TabIndex = 0;
            PCT_CANVAS.TabStop = false;
            PCT_CANVAS.Click += PCT_CANVAS_Click;
            PCT_CANVAS.Paint += PCT_CANVAS_Paint;
            PCT_CANVAS.MouseClick += PCT_CANVAS_MouseClick;
            PCT_CANVAS.MouseDown += PCT_CANVAS_MouseDown;
            PCT_CANVAS.MouseMove += PCT_CANVAS_MouseMove;
            PCT_CANVAS.MouseUp += PCT_CANVAS_MouseUp;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 451);
            Controls.Add(label1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(PCT_CANVAS);
            KeyPreview = true;
            Name = "Form1";
            Text = "Form1";
            WindowState = FormWindowState.Maximized;
            Load += Form1_Load;
            KeyDown += Form1_KeyDown;
            ((System.ComponentModel.ISupportInitialize)PCT_CANVAS).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Timer TIMER;
        private Label label2;
        private Label label3;
        private Label label1;
        private PictureBox PCT_CANVAS;
    }
}
