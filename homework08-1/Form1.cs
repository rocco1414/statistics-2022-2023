using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace homework08_1
{
    public partial class Form1 : Form
    {
        Random r = new Random();
        Pen pen = new Pen(Color.Green);
        Bitmap b;
        Graphics g;


        Bitmap b2;
        Graphics g2;

        Bitmap b3;
        Graphics g3;
        public Form1()
        {
            InitializeComponent();
        }

        private int FromXRealToXVirtual(double X, double minX, double maxX, int Left, int W)
        {
            if (maxX - minX == 0)
            {
                return 0;
            }
            else
            {
                return (int)(Left + W * (X - minX) / (maxX - minX));
            }
        }

        private int FromYRealToYVirtual(double Y, double minY, double maxY, int Top, int H)
        {
            if (maxY - minY == 0)
            {
                return 0;
            }
            else
            {
                return (int)(Top + H - H * (Y - minY) / (maxY - minY));
            }
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            this.b = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            this.g = Graphics.FromImage(b);

            this.g.Clear(Color.White);

            Rectangle window = new Rectangle(0, 0, this.b.Width - 1, this.b.Height - 1);
            g.DrawRectangle(Pens.Black, window);

            int nPoints = (int)trackBar1.Value;

            double minX = -100;
            double maxX = 100;
            double minY = -100;
            double maxY = 100;

            List<double> X = new List<double>();
            List<double> Y = new List<double>();


            for (int i = 0; i < nPoints; i++)
            {
                double radius = r.NextDouble() * 100;
                double angle = r.Next(0, 360);
                double xCoord = radius * Math.Cos(angle);
                double yCoord = radius * Math.Sin(angle);

                int xDevice = FromXRealToXVirtual(xCoord, minX, maxX, window.Left, window.Width);
                int yDevice = FromYRealToYVirtual(yCoord, minY, maxY, window.Top, window.Height);

                Rectangle rectangle = new Rectangle(xDevice, yDevice, 1, 1);
                g.DrawRectangle(pen, rectangle);
                g.FillRectangle(Brushes.Black, rectangle);

                X.Add(xDevice);
                Y.Add(yDevice);

            }

            this.pictureBox1.Image = b;

            this.b2 = new Bitmap(this.pictureBox2.Width, this.pictureBox2.Height);
            this.g2 = Graphics.FromImage(b2);
            this.g2.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.g2.Clear(Color.White);

            Rectangle Window2 = new Rectangle(0, 0, this.b2.Width - 1, this.b2.Height - 1);
            g2.DrawRectangle(Pens.Black, Window2);

            double minValueX = X.Min();
            double maxValueX = X.Max();
            double delta = maxValueX - minValueX;
            double nIntervals = 15;
            double sizeIntervalX = delta / nIntervals;

            Dictionary<double, int> XHinstogram = new Dictionary<double, int>();

            double tempValue = minValueX;
            for (int i = 0; i < nIntervals; i++)
            {
                XHinstogram[tempValue] = 0;
                tempValue = tempValue + sizeIntervalX;
            }

            int tot = 0;

            foreach (double value in X)
            {
                foreach (double key in XHinstogram.Keys)
                {
                    if (value < key + sizeIntervalX)
                    {
                        XHinstogram[key] += 1;
                        if (tot < XHinstogram[key])
                        {
                            tot = XHinstogram[key];
                        }
                        break;
                    }
                }
            }

            g2.TranslateTransform(0, this.b2.Height);
            g2.ScaleTransform(1, -1);

            int idHistogram = 0;
            int widthHistogram = (int)(this.b2.Width / nIntervals);
            double lastKey = 0;

            foreach (double key in XHinstogram.Keys)
            {
                lastKey = key;
                int newHeight = XHinstogram[key] * this.b2.Height / tot;
                int newX = (widthHistogram * idHistogram) + 1;
                Rectangle histogram = new Rectangle(newX, 0, widthHistogram, newHeight);
                idHistogram++;


                g2.DrawRectangle(Pens.Black, histogram);
                g2.FillRectangle(Brushes.Green, histogram);
            }


            this.pictureBox2.Image = b2;


            this.b3 = new Bitmap(this.pictureBox3.Width, this.pictureBox3.Height);
            this.g3 = Graphics.FromImage(b3);
            this.g3.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.g3.Clear(Color.White);

            Rectangle VirtualWindow3 = new Rectangle(0, 0, this.b3.Width - 1, this.b3.Height - 1);
            g3.DrawRectangle(Pens.Black, VirtualWindow3);

            double minValueY = Y.Min();
            double maxValueY = Y.Max();
            double deltaY = maxValueY - minValueY;
            double sizeIntervalY = deltaY / nIntervals;

            Dictionary<double, int> YHistogram = new Dictionary<double, int>();

            double tempValueY = minValueY;
            for (int i = 0; i < nIntervals; i++)
            {
                YHistogram[tempValueY] = 0;
                tempValueY = tempValueY + sizeIntervalY;
            }

            int totalY = 0;

            foreach (double value in Y)
            {
                foreach (double key in YHistogram.Keys)
                {
                    if (value < key + sizeIntervalY)
                    {
                        YHistogram[key] += 1;
                        if (totalY < YHistogram[key])
                        {
                            totalY = YHistogram[key];
                        }
                        break;
                    }
                }
            }

            g3.TranslateTransform(0, this.b3.Height);
            g3.ScaleTransform(1, -1);

            idHistogram = 0;
            int widthHistogramY = (int)(this.b3.Width / nIntervals);
            double lastKeyY = 0;

            foreach (double key in YHistogram.Keys)
            {
                lastKeyY = key;
                int newHeight = YHistogram[key] * this.b3.Height / totalY;
                int newX = (widthHistogramY * idHistogram) + 1;
                Rectangle isto = new Rectangle(newX, 0, widthHistogramY, newHeight);
                idHistogram++;

                g3.DrawRectangle(Pens.Black, isto);
                g3.FillRectangle(Brushes.Green, isto);


            }


            this.pictureBox3.Image = b3;


        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label1.Text = trackBar1.Value.ToString();
        }
    }
}
