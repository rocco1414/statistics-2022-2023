using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace homework08_2
{
    public partial class Form1 : Form
    {
        Random r = new Random();
        Pen p = new Pen(Color.Green);
        Bitmap b;
        Graphics g;
        int numberOftrials = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.setUpWindow();
            double minValue = -3;
            double maxValue = 3;
            double delta = maxValue - minValue;
            double numberOfInterval = 150;
            double sizeOfInterval = delta / numberOfInterval;

            Dictionary<double, int> histogramDict = new Dictionary<double, int>();
            double tempValue = minValue;
            for (int i = 0; i <= numberOfInterval; i++)
            {
                tempValue = minValue + (sizeOfInterval * i);
                tempValue = Math.Round(tempValue, 2);
                histogramDict[tempValue] = 0;
            }

            int total = 0;

            for (int x = 0; x < numberOftrials; x++)
            {
                double xRnd = (r.NextDouble() * 2) - 1;
                double value1 = 0;


                double yRnd = (r.NextDouble() * 2) - 1;

                double s = (xRnd * xRnd) + (yRnd * yRnd);

                while (s < 0 || s > 1)
                {
                    xRnd = (r.NextDouble() * 2) - 1;
                    yRnd = (r.NextDouble() * 2) - 1;
                    s = (xRnd * xRnd) + (yRnd * yRnd);
                }

                xRnd = xRnd * Math.Sqrt(-2 * Math.Log(s) / s);
                yRnd = yRnd * Math.Sqrt(-2 * Math.Log(s) / s);

                value1 = xRnd;


                foreach (double key in histogramDict.Keys)
                {
                    double range = key + sizeOfInterval;
                    if (range > maxValue) range = maxValue;
                    if (value1 < range && value1 > key)
                    {
                        histogramDict[key] += 1;
                        if (total < histogramDict[key])
                        {
                            total = histogramDict[key];
                        }
                        break;
                    }
                }
            }
            g.TranslateTransform(0, this.b.Height);
            g.ScaleTransform(1, -1);

            int idIstogram = 0;
            int widthIstogram = (int)(this.b.Width / numberOfInterval);
            double lastKeyY = 0;

            foreach (double key in histogramDict.Keys)
            {
                lastKeyY = key;
                int newHeight = histogramDict[key] * this.b.Height / total;
                int newX = (widthIstogram * idIstogram) + 10;
                Rectangle isto = new Rectangle(newX, 0, widthIstogram, newHeight);
                idIstogram++;

                int nextWidthIstogram = (int)(widthIstogram * idIstogram * 1);

                g.DrawRectangle(Pens.Green, isto);
                g.FillRectangle(Brushes.Green, isto);
            }
            this.pictureBox1.Image = b;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label1.Text = trackBar1.Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.setUpWindow();

            double minValue = 0;
            double maxValue = 4;

            double delta = maxValue - minValue;
            double nintervals = 150;
            double intervalsSize = delta / nintervals;

            Dictionary<double, int> istogramDict = new Dictionary<double, int>();
            double tempValue = minValue;
            for (int i = 0; i <= nintervals; i++)
            {
                tempValue = minValue + (intervalsSize * i);
                tempValue = Math.Round(tempValue, 2);
                istogramDict[tempValue] = 0;
            }

            int total = 0;

            for (int x = 0; x < this.numberOftrials; x++)
            {
                double xRnd = (r.NextDouble() * 2) - 1;
                double value2 = 0;
                double yRnd = (r.NextDouble() * 2) - 1;
                double s = (xRnd * xRnd) + (yRnd * yRnd);

                while (s < 0 || s > 1)
                {
                    xRnd = (r.NextDouble() * 2) - 1;
                    yRnd = (r.NextDouble() * 2) - 1;
                    s = (xRnd * xRnd) + (yRnd * yRnd);
                }

                xRnd = xRnd * Math.Sqrt(-2 * Math.Log(s) / s);
                yRnd = yRnd * Math.Sqrt(-2 * Math.Log(s) / s);
                value2 = xRnd * xRnd;

                foreach (double key in istogramDict.Keys)
                {
                    double range = key + intervalsSize;
                    if (range > maxValue) range = maxValue;
                    if (value2 < range && value2 > key)
                    {
                        istogramDict[key] += 1;
                        if (total < istogramDict[key])
                        {
                            total = istogramDict[key];
                        }
                        break;
                    }
                }
            }
            g.TranslateTransform(0, this.b.Height);
            g.ScaleTransform(1, -1);

            int idIstogram = 0;
            int widthIstogram = (int)(this.b.Width / nintervals);
            double lastKeyY = 0;

            foreach (double key in istogramDict.Keys)
            {
                lastKeyY = key;
                int newHeight = istogramDict[key] * this.b.Height / total;
                int newX = (widthIstogram * idIstogram) + 10;
                Rectangle isto = new Rectangle(newX, 0, widthIstogram, newHeight);
                idIstogram++;

                int nextWidthIstogram = (int)(widthIstogram * idIstogram * 1);

                g.DrawRectangle(Pens.Green, isto);
                g.FillRectangle(Brushes.Green, isto);

            }

            this.pictureBox1.Image = b;
        }

        private void setUpWindow()
        {
            this.b = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            this.g = Graphics.FromImage(b);
            this.g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.g.Clear(Color.White);

            Rectangle window1 = new Rectangle(0, 0, this.b.Width - 1, this.b.Height - 1);
            g.DrawRectangle(Pens.Black, window1);
            numberOftrials = (int)trackBar1.Value;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.setUpWindow();
            double minValue = -8;
            double maxValue = 8;
            double delta = maxValue - minValue;
            double nintervals = 150;
            double intervalsSize = delta / nintervals;

            Dictionary<double, int> istogramDict = new Dictionary<double, int>();
            double tempValue = minValue;
            for (int i = 0; i <= nintervals; i++)
            {
                tempValue = minValue + (intervalsSize * i);
                tempValue = Math.Round(tempValue, 2);
                istogramDict[tempValue] = 0;
            }

            int total = 0;

            for (int x = 0; x < this.numberOftrials; x++)
            {
                double xRnd = (r.NextDouble() * 2) - 1;
                double value3 = 0;
                double yRnd = (r.NextDouble() * 2) - 1;
                double s = (xRnd * xRnd) + (yRnd * yRnd);

                while (s < 0 || s > 1)
                {
                    xRnd = (r.NextDouble() * 2) - 1;
                    yRnd = (r.NextDouble() * 2) - 1;
                    s = (xRnd * xRnd) + (yRnd * yRnd);
                }

                xRnd = xRnd * Math.Sqrt(-2 * Math.Log(s) / s);
                yRnd = yRnd * Math.Sqrt(-2 * Math.Log(s) / s);

                value3 = xRnd / (yRnd * yRnd);

                foreach (double key in istogramDict.Keys)
                {
                    double range = key + intervalsSize;
                    if (range > maxValue) range = maxValue;
                    if (value3 < range && value3 > key)
                    {
                        istogramDict[key] += 1;
                        if (total < istogramDict[key])
                        {
                            total = istogramDict[key];
                        }
                        break;
                    }
                }
            }
            g.TranslateTransform(0, this.b.Height);
            g.ScaleTransform(1, -1);

            int idIstogram = 0;
            int widthIstogram = (int)(this.b.Width / nintervals);
            double lastKeyY = 0;

            foreach (double key in istogramDict.Keys)
            {
                lastKeyY = key;
                int newHeight = istogramDict[key] * this.b.Height / total;
                int newX = (widthIstogram * idIstogram) + 10;
                Rectangle isto = new Rectangle(newX, 0, widthIstogram, newHeight);
                idIstogram++;

                int nextWidthIstogram = (int)(widthIstogram * idIstogram * 1);

                g.DrawRectangle(Pens.Green, isto);
                g.FillRectangle(Brushes.Green, isto);

            }
            this.pictureBox1.Image = b;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.setUpWindow();

            double minValue = 0;
            double maxValue = 4;
            double delta = maxValue - minValue;
            double nintervals = 150;
            double intervalsSize = delta / nintervals;

            Dictionary<double, int> istogramDict = new Dictionary<double, int>();
            double tempValue = minValue;
            for (int i = 0; i <= nintervals; i++)
            {
                tempValue = minValue + (intervalsSize * i);
                tempValue = Math.Round(tempValue, 2);
                istogramDict[tempValue] = 0;
            }

            int total = 0;

            for (int x = 0; x < this.numberOftrials; x++)
            {
                double xRnd = (r.NextDouble() * 2) - 1;
                double value4 = 0;
                double yRnd = (r.NextDouble() * 2) - 1;
                double s = (xRnd * xRnd) + (yRnd * yRnd);

                while (s < 0 || s > 1)
                {
                    xRnd = (r.NextDouble() * 2) - 1;
                    yRnd = (r.NextDouble() * 2) - 1;
                    s = (xRnd * xRnd) + (yRnd * yRnd);
                }

                xRnd = xRnd * Math.Sqrt(-2 * Math.Log(s) / s);
                yRnd = yRnd * Math.Sqrt(-2 * Math.Log(s) / s);


                value4 = (xRnd * xRnd) / (yRnd * yRnd);

                foreach (double key in istogramDict.Keys)
                {
                    double range = key + intervalsSize;
                    if (range > maxValue) range = maxValue;
                    if (value4 < range && value4 > key)
                    {
                        istogramDict[key] += 1;
                        if (total < istogramDict[key])
                        {
                            total = istogramDict[key];
                        }
                        break;
                    }
                }
            }

            g.TranslateTransform(0, this.b.Height);
            g.ScaleTransform(1, -1);

            int idIstogram = 0;
            int widthIstogram = (int)(this.b.Width / nintervals);
            double lastKeyY = 0;

            foreach (double key in istogramDict.Keys)
            {
                lastKeyY = key;
                int newHeight = istogramDict[key] * this.b.Height / total;
                int newX = (widthIstogram * idIstogram) + 10;
                Rectangle isto = new Rectangle(newX, 0, widthIstogram, newHeight);
                idIstogram++;

                int nextWidthIstogram = (int)(widthIstogram * idIstogram * 1);

                g.DrawRectangle(Pens.Green, isto);
                g.FillRectangle(Brushes.Green, isto);

            }
            this.pictureBox1.Image = b;
        }

        private void button5_Click(object sender, EventArgs e)
        {    
            this.setUpWindow();
            double minValue = -10;
            double maxValue = 10;
            double delta = maxValue - minValue;
            double nintervals = 150;
            double intervalsSize = delta / nintervals;

            Dictionary<double, int> istogramDict = new Dictionary<double, int>();
            double tempValue = minValue;
            for (int i = 0; i <= nintervals; i++)
            {
                tempValue = minValue + (intervalsSize * i);
                tempValue = Math.Round(tempValue, 2);
                istogramDict[tempValue] = 0;
            }

            int total = 0;

            for (int x = 0; x < this.numberOftrials; x++)
            {
                double xRnd = (r.NextDouble() * 2) - 1;
                double value5 = 0;
                double yRnd = (r.NextDouble() * 2) - 1;
                double s = (xRnd * xRnd) + (yRnd * yRnd);

                while (s < 0 || s > 1)
                {
                    xRnd = (r.NextDouble() * 2) - 1;
                    yRnd = (r.NextDouble() * 2) - 1;
                    s = (xRnd * xRnd) + (yRnd * yRnd);
                }

                xRnd = xRnd * Math.Sqrt(-2 * Math.Log(s) / s);
                yRnd = yRnd * Math.Sqrt(-2 * Math.Log(s) / s);
                value5 = xRnd / yRnd;

                foreach (double key in istogramDict.Keys)
                {
                    double range = key + intervalsSize;
                    if (range > maxValue) range = maxValue;
                    if (value5 < range && value5 > key)
                    {
                        istogramDict[key] += 1;
                        if (total < istogramDict[key])
                        {
                            total = istogramDict[key];
                        }
                        break;
                    }
                }
            }
            g.TranslateTransform(0, this.b.Height);
            g.ScaleTransform(1, -1);

            int idIstogram = 0;
            int widthIstogram = (int)(this.b.Width / nintervals);
            double lastKeyY = 0;

            foreach (double key in istogramDict.Keys)
            {
                lastKeyY = key;
                int newHeight = istogramDict[key] * this.b.Height / total;
                int newX = (widthIstogram * idIstogram) + 1;
                Rectangle isto = new Rectangle(newX, 0, widthIstogram, newHeight);
                idIstogram++;

                int nextWidthIstogram = (int)(widthIstogram * idIstogram * 1);

                g.DrawRectangle(Pens.Green, isto);
                g.FillRectangle(Brushes.Green, isto);

            }
            this.pictureBox1.Image = b;
        }
    }
}
