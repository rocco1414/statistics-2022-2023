using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace homework05_histogram
{
    public partial class Form1 : Form
    {

        
        public Graphics g;
        public Bitmap b;
        int[] list;
        public Random r = new Random();

        int width = 300;
        int height = 400;

        

        private void initialize()
        {
            this.b = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            this.g = Graphics.FromImage(b);
        }


        //Random distribution
        private void generateDistribution()
        {
            Random rand = new Random();
            list = new int[100];
            for (int i = 0; i < 100; i++)
                list[i] = rand.Next(1, 100);
        }

        //rectangle
        private void createRectangle()
        {
            g.DrawRectangle(Pens.Black, 10, 10, width, height);
            pictureBox1.Image = b;
            pictureBox1.Refresh();
        }

        public Form1()
        {
            InitializeComponent();
            initialize();
        }

        //private int FromXRealToXVirtual(int X, int minX, int maxX, int W)
        //{
        //    return W * (X - minX) / (maxX - minX);
        //}

        private int fromXRealToXVirtual(double x, double minX, double maxX, int left, int w)
        {
            return left + (int)(w * (x - minX) / (maxX - minX));
        }

        private int fromYRealToYVirtual(double y, double minY, double maxY, int top, int h)
        {
            return top + (int)(h - h * (y - minY) / (maxY - minY));
        }

        private void displayDistribution()
        {
            int x = 10;
            int y = 10;
            int max = list.Max();
            int min = list.Min();
            int range = max - min;

            if (this.checkBox1.Checked)
            { // Vertical
                int barWidth = width / 100;
                int barHeight = 0;
                int scale = height / range;

                for (int i = 0; i < 100; i++)
                {
                    barHeight = list[i] * scale;
                    g.DrawRectangle(Pens.Black, x, y + height - barHeight, barWidth, barHeight);
                    g.FillRectangle(new SolidBrush(Color.FromArgb(64, 0, 255, 0)), x, y + height - barHeight, barWidth, barHeight);
                    x += barWidth;
                }
            }
            else
            { // Horizontal
                int barHeight = height / 100;
                int barWidth = 0;
                int scale = width / range;

                for (int i = 0; i < 100; i++)
                {
                    barWidth = list[i] * scale;
                    g.DrawRectangle(Pens.Black, x, y, width - barWidth, barHeight);
                    g.FillRectangle(new SolidBrush(Color.FromArgb(64, 0, 255, 0)), x, y, width - barWidth, barHeight);
                    y += barHeight;
                }
            }

            pictureBox1.Image = b;
            pictureBox1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            generateDistribution();
            createRectangle();
            displayDistribution();
        }
    }
}
