using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace homework07
{
    public partial class Form1 : Form
    {

        public Bitmap b;
        public Graphics g;
        public Bitmap b2;
        public Graphics g2;
        public Bitmap b3;
        public Graphics g3;
        public Random random = new Random();
        public Pen PenTrajectory = new Pen(Color.Red, 2);
        public Pen PenTrajectory2 = new Pen(Color.Black, 2);

        private List<int> FrequOflastTry;
        public Form1()
        {
            InitializeComponent();
        }


        public void vertical(Bitmap b, Graphics g, PictureBox pictureBox, Dictionary<int, int> distr, int numElement)
        {
            int j = 0;
            int step1 = (int)(pictureBox.Width / (double)distr.Count);

            g.DrawRectangle(PenTrajectory2, 0, 0, pictureBox.Width - 1, pictureBox.Height - 1);

            foreach (KeyValuePair<int, int> pair in distr)
            {
                double virtualX = FromXRealToXVirtual(pair.Value, 0, numElement, 0, pictureBox.Height);
                Rectangle r = new Rectangle(j + 1, System.Convert.ToInt32(pictureBox.Height - (virtualX) - 1), step1, (int)virtualX);
                g.DrawRectangle(PenTrajectory2, j + 1, System.Convert.ToInt32(pictureBox.Height - (virtualX) - 1), step1, (int)virtualX);
                Brush x2 = new SolidBrush(Color.Blue);
                g.FillRectangle(x2, r);
                j = j + step1;
            }

            pictureBox.Image = b;
        }


        public double FromXRealToXVirtual(double X, double minX, double maxX, int Left, int W)
        {
            if ((maxX - minX) == 0)
                return 0;

            return Left + W * (X - minX) / (maxX - minX);
        }

        public double FromYRealToYVirtual(double Y, double minY, double maxY, int Top, int H)
        {
            if ((maxY - minY) == 0)
                return 0;

            return Top + H - H * (Y - minY) / (maxY - minY);
        }

        private void Compute_Click(object sender, EventArgs e)
        {
            this.b = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            this.g = Graphics.FromImage(b);
            this.g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.g.Clear(Color.White);
            this.b2 = new Bitmap(this.pictureBox2.Width, this.pictureBox2.Height);
            this.g2 = Graphics.FromImage(b2);
            this.g2.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.g2.Clear(Color.White);
            this.b3 = new Bitmap(this.pictureBox3.Width, this.pictureBox3.Height);
            this.g3 = Graphics.FromImage(b3);
            this.g3.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.g3.Clear(Color.White);

            var interarrival = 0;
            Dictionary<int, int> interrarivalDistribution = new Dictionary<int, int>();
            int TrialsCount = (int)numericUpDown2.Value;
            int NumerOfTrajectories = (int)numericUpDown1.Value;
            double SuccessProbability = (double)UpDownProbability.Value / (double)TrialsCount;

            double minX = 0;
            double maxX = TrialsCount;
            double minY = 0;
            double maxY = TrialsCount;


            Rectangle VirtualWindow = new Rectangle(20, 20, this.b.Width - 40, this.b.Height - 40);

            g.DrawRectangle(Pens.DarkSlateGray, VirtualWindow);

            int[] altezze = new int[this.pictureBox1.Height + 1];
            for (int i = 0; i <= this.pictureBox1.Height; i++)
                altezze[i] = 0;

            for (int i = 1; i <= NumerOfTrajectories; i++)
            {
                List<Point> Punti = new List<Point>();
                double Y = 0;
                for (int X = 1; X <= TrialsCount; X++)
                {
                    double Uniform = random.NextDouble();
                    if (Uniform < SuccessProbability)
                    {
                        Y = Y + 1;
                        if (interarrival != 0)
                        {
                            if (!interrarivalDistribution.ContainsKey(interarrival))
                                interrarivalDistribution.Add(interarrival, 1);
                            else
                                interrarivalDistribution[interarrival] = interrarivalDistribution[interarrival] + 1;
                            interarrival = 0;
                        }
                    }
                    else
                        interarrival = interarrival + 1;
                    double xDevice = FromXRealToXVirtual(X, minX, maxX, VirtualWindow.Left, VirtualWindow.Width);
                    double YDevice = FromYRealToYVirtual(Y, minY, maxY, VirtualWindow.Top, VirtualWindow.Height);
                    Punti.Add(new Point((int)xDevice, (int)YDevice));
                }
                g.DrawLines(PenTrajectory, Punti.ToArray());
                altezze[Punti.Last().Y] += 100;
            }

            Pen pen = new Pen(Color.Black, 5);
            this.pictureBox1.Image = b;
            this.pictureBox2.Image = b2;

            for (int i = 0; i <= altezze.Length - 1; i++)

                g2.DrawLine(pen, 0, i, altezze[i], i);


            vertical(b3, g3, pictureBox3, interrarivalDistribution, TrialsCount);
            pictureBox3.Image = b3;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.b = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            this.b2 = new Bitmap(this.pictureBox2.Width, this.pictureBox2.Height);
            this.g = Graphics.FromImage(b);
            this.g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.g.Clear(Color.White);
            this.g2 = Graphics.FromImage(b2);
            this.g2.Clear(Color.White);
            this.g2.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.b3 = new Bitmap(this.pictureBox3.Width, this.pictureBox3.Height);
            this.g3 = Graphics.FromImage(b3);
            this.g3.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.g3.Clear(Color.White);

            var interarrival = 0;
            Dictionary<int, int> interrarivalDistribution = new Dictionary<int, int>();

            int TrialsCount = (int)numericUpDown2.Value;
            int NumerOfTrajectories = (int)numericUpDown1.Value;
            double SuccessProbability = (double)UpDownProbability.Value / (double)TrialsCount;

            double minX = 0;
            double maxX = TrialsCount;
            double minY = 0;
            double maxY = TrialsCount;

            Rectangle VirtualWindow = new Rectangle(20, 20, this.b.Width - 40, this.b.Height - 40);

            int[] altezze = new int[this.pictureBox1.Height + 1];
            for (int i = 0; i <= this.pictureBox1.Height; i++)
                altezze[i] = 0;

            g.DrawRectangle(Pens.DarkSlateGray, VirtualWindow);

            for (int i = 1; i <= NumerOfTrajectories; i++)
            {
                List<Point> Punti = new List<Point>();
                double Y = 0;
                for (int X = 1; X <= TrialsCount; X++)
                {
                    double Uniform = random.NextDouble();
                    if (Uniform < SuccessProbability)
                    {
                        Y = Y + 1;
                        if (interarrival != 0)
                        {
                            if (!interrarivalDistribution.ContainsKey(interarrival))
                                interrarivalDistribution.Add(interarrival, 1);
                            else
                                interrarivalDistribution[interarrival] = interrarivalDistribution[interarrival] + 1;
                            interarrival = 0;
                        }
                    }
                    else
                        interarrival = interarrival + 1;
                    double xDevice = FromXRealToXVirtual(X, minX, maxX, VirtualWindow.Left, VirtualWindow.Width);
                    double YDevice = FromYRealToYVirtual(Y * TrialsCount / (X + 1), minY, maxY, VirtualWindow.Top, VirtualWindow.Height);
                    Punti.Add(new Point((int)xDevice, (int)YDevice));
                }
                g.DrawLines(PenTrajectory, Punti.ToArray());
                altezze[Punti.Last().Y] += 100;
            }

            Pen pen = new Pen(Color.Black, 5);
            this.pictureBox1.Image = b;
            this.pictureBox2.Image = b2;
            for (int i = 0; i <= altezze.Length - 1; i++)

                g2.DrawLine(pen, 0, i, altezze[i], i);

            this.pictureBox1.Image = b;

            vertical(b3, g3, pictureBox3, interrarivalDistribution, TrialsCount);
            pictureBox3.Image = b3;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.b = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            this.b2 = new Bitmap(this.pictureBox2.Width, this.pictureBox2.Height);
            this.g = Graphics.FromImage(b);
            this.g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.g.Clear(Color.White);
            this.g2 = Graphics.FromImage(b2);
            this.g2.Clear(Color.White);
            this.g2.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.b3 = new Bitmap(this.pictureBox3.Width, this.pictureBox3.Height);
            this.g3 = Graphics.FromImage(b3);
            this.g3.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.g3.Clear(Color.White);

            var interarrival = 0;
            Dictionary<int, int> interrarivalDistribution = new Dictionary<int, int>();

            int TrialsCount = (int)numericUpDown2.Value;
            int NumerOfTrajectories = (int)numericUpDown1.Value;
            double SuccessProbability = (double)UpDownProbability.Value / (double)TrialsCount;

            double minX = 0;
            double maxX = TrialsCount;
            double minY = 0;
            double maxY = TrialsCount;

            Rectangle VirtualWindow = new Rectangle(20, 20, this.b.Width - 40, this.b.Height - 40);

            int[] altezze = new int[this.pictureBox1.Height + 1];
            for (int i = 0; i <= this.pictureBox1.Height; i++)
                altezze[i] = 0;

            g.DrawRectangle(Pens.DarkSlateGray, VirtualWindow);

            for (int i = 1; i <= NumerOfTrajectories; i++)
            {
                List<Point> Punti = new List<Point>();
                double Y = 0;
                for (int X = 1; X <= TrialsCount; X++)
                {
                    double Uniform = random.NextDouble();
                    if (Uniform < SuccessProbability)
                    {
                        Y = Y + 1;
                        if (interarrival != 0)
                        {
                            if (!interrarivalDistribution.ContainsKey(interarrival))
                                interrarivalDistribution.Add(interarrival, 1);
                            else
                                interrarivalDistribution[interarrival] = interrarivalDistribution[interarrival] + 1;
                            interarrival = 0;
                        }
                    }
                    else
                        interarrival = interarrival + 1;
                    double xDevice = FromXRealToXVirtual(X, minX, maxX, VirtualWindow.Left, VirtualWindow.Width);
                    double YDevice = FromYRealToYVirtual(Y * Math.Sqrt(TrialsCount) / Math.Sqrt(X + 1), minY, maxY, VirtualWindow.Top, VirtualWindow.Height);
                    Punti.Add(new Point((int)xDevice, (int)YDevice));
                }
                g.DrawLines(PenTrajectory, Punti.ToArray());
                altezze[Punti.Last().Y] += 100;
            }

            Pen pen = new Pen(Color.Black, 5);
            this.pictureBox1.Image = b;
            this.pictureBox2.Image = b2;
            for (int i = 0; i <= altezze.Length - 1; i++)

                g2.DrawLine(pen, 0, i, altezze[i], i);

            this.pictureBox1.Image = b;

            vertical(b3, g3, pictureBox3, interrarivalDistribution, TrialsCount);
            pictureBox3.Image = b3;
        }
    }
}
