using System;
using System.Drawing;
using System.Windows.Forms;

namespace FlowerAnimation
{
    public partial class Form1 : Form
    {
        private Timer animationTimer;
        private float flowerScale = 0f;
        private float stemHeight = 0f;
        private int frameCount = 0;

        public Form1()
        {
            InitializeComponent();

            // Set up the form
            this.Text = "Flower Animation";
            this.Size = new Size(600, 800);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Set up the timer for the animation
            animationTimer = new Timer();
            animationTimer.Interval = 50; // Interval in milliseconds
            animationTimer.Tick += AnimationTimer_Tick;
            animationTimer.Start();
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            frameCount++;

            // Gradually increase stem height
            if (frameCount <= 100)
            {
                stemHeight += 0.03f; // Increase height over time
            }

            // Gradually increase flower scale to simulate blooming
            if (frameCount > 20 && frameCount <= 120)
            {
                flowerScale += 0.03f;
            }

            // Redraw the form
            this.Invalidate();

            // Stop animation after 120 frames
            if (frameCount > 120)
            {
                animationTimer.Stop();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            // Set up the drawing area
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Draw the stem (growing)
            Pen stemPen = new Pen(Color.Green, 6);
            g.DrawLine(stemPen, 300, 700, 300, 700 - stemHeight);

            // Draw the flower (growing)
            if (flowerScale > 0)
            {
                Brush flowerBrush = new SolidBrush(Color.Red);
                // Draw flower petals as ellipses (circle shapes)
                for (int i = 0; i < 5; i++)
                {
                    double angle = i * 2 * Math.PI / 5;
                    float petalX = 300 + (float)(flowerScale * 50 * Math.Cos(angle));
                    float petalY = 700 - stemHeight + (float)(flowerScale * 50 * Math.Sin(angle));
                    g.FillEllipse(flowerBrush, petalX - 20, petalY - 20, 40, 40);
                }

                // Draw flower center
                g.FillEllipse(Brushes.Yellow, 300 - 20, 700 - stemHeight - 20, 40, 40);
            }
        }
    }
}
