using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Homework04
{
    public partial class Form1 : Form
    {
        private Bitmap bitmap;
        private Graphics graphics;
        private Random random = new Random();
        private Pen PenTrajectoryRelative = new Pen(Color.Red, (float)0.22);
        private Pen PenTrajectoryAbsolute = new Pen(Color.Yellow, (float)0.22);
        private Pen PenTrajectoryNormalized = new Pen(Color.Blue, (float)0.22);

        private Bitmap bitmapHistogram;
        private Graphics graphicsHistogram;

        private List<int> FrequOflastTry;

        public Form1()
        {
            InitializeComponent();
        }


        private int X_ViewPort(double X_world, Rectangle viewPort, double minX_window, double rangeX)
        {
            if (rangeX == 0)
            {
                return 0;
            } else
            {
                return (int)(viewPort.Left + ((X_world - minX_window) * viewPort.Width) / rangeX);
            }
        }


        private int Y_ViewPort(double Y_world, Rectangle viewPort, double min_Y, double rangeY)
        {
            if (rangeY == 0)
            {
                return 0;
            } else
            {
                return (int)(viewPort.Top + viewPort.Height - ((Y_world - min_Y) * viewPort.Height) / rangeY);

            }
        }

        private List<int> generateBernoulliDistribution(int trials, double probability)
        {

            List<int> distribution = new List<int>();
            for (int i = 0; i <= trials; i++)
            {
                double randomValue = random.NextDouble();
                if (randomValue <= probability)
                {
                    distribution.Add(1);
                } else
                {
                    distribution.Add(0);
                }
            }
            return distribution;
        }

        
        private void computeSqeuences (){
            this.bitmap = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            this.graphics = Graphics.FromImage(bitmap);
            this.graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.graphics.Clear(Color.White);

            int NTrials = (int)UpDownTrials.Value;
            int NTrajectories = (int)UpDownTrajectories.Value;
            double NProbability = Convert.ToDouble(UpDownProbability.Value) / 100;

            //Window
            double minX = 0;
            double MaxX = NTrials;
            double minY = 0;
            double MaxY = NTrials;
            double rangX = MaxX - minX;
            double rangY = MaxY - minY;

            double MaxYAbs = NTrials / Math.Sqrt(NTrials);
            double rangAbs = MaxYAbs - minY;

            //ViewPort
            Rectangle viewPort = new Rectangle(0, 0, this.bitmap.Width - 1, this.bitmap.Width - 1);
            graphics.DrawRectangle(Pens.Black, viewPort);

            

            for (int i = 0; i < NTrajectories; i++)
            {
                // I obtain something like this 0,0,1,1,1,0,0,1 (1 is success)
                List<int> BernoulliDistribution = this.generateBernoulliDistribution(NTrials, NProbability);
                List<Point> AbsolutePoint = new List<Point>();
                List<Point> RelativePoint = new List<Point>();
                List<Point> NormalizedPoint = new List<Point>();

                //X-assis number of tries
                //Y-assis frequncy
                int currentTry = 0;
                int currentSuccess = 0;
                foreach (int x in BernoulliDistribution)
                {
                    currentTry++;
                    if (x == 1)
                    {
                        currentSuccess++;
                    }

                    double RelativeFreq = currentSuccess * NTrials / currentTry;

                    int XDevice = X_ViewPort(currentTry - 1, viewPort, minX, rangX);
                    int YDeviceFreqRel = Y_ViewPort(RelativeFreq, viewPort, minY, rangY);
                    RelativePoint.Add(new Point(XDevice, YDeviceFreqRel));

                    int YDeviceFreqAbsolute = Y_ViewPort(currentSuccess, viewPort, minY, rangY);
                    AbsolutePoint.Add(new Point(XDevice, YDeviceFreqAbsolute));


                    double YNormalized = currentSuccess / Math.Sqrt(currentTry);
                    int YDeviceFreqNormalized = Y_ViewPort(YNormalized, viewPort, minY, rangAbs);
                    NormalizedPoint.Add(new Point(XDevice, YDeviceFreqNormalized));

                    if (currentTry == NTrials)
                    {
                        FrequOflastTry.Add(YDeviceFreqRel);
                        FrequOflastTry.Add(YDeviceFreqAbsolute);
                        FrequOflastTry.Add(YDeviceFreqNormalized);
                        this.richTextBox3.AppendText("R: " + YDeviceFreqRel);
                        this.richTextBox3.AppendText("A: " + YDeviceFreqAbsolute);
                        this.richTextBox3.AppendText("N: " + YDeviceFreqNormalized + "\n");
                    }
                }


                graphics.DrawLines(PenTrajectoryRelative, RelativePoint.ToArray());
                graphics.DrawLines(PenTrajectoryAbsolute, AbsolutePoint.ToArray());
                graphics.DrawLines(PenTrajectoryNormalized, NormalizedPoint.ToArray());
            }


            this.pictureBox1.Image = bitmap;
        }

        private void doHistogram()
        {
            this.bitmapHistogram = new Bitmap(this.pictureBox2.Width, this.pictureBox2.Height);
            this.graphicsHistogram = Graphics.FromImage(bitmapHistogram);
            this.graphicsHistogram.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.graphicsHistogram.Clear(Color.White);

            Dictionary<double, int> intervals = new Dictionary<double, int>();

            double startingInter = 2.5;
            double inter = startingInter;
            while (inter <= this.bitmapHistogram.Height)
            {
                
                intervals[inter] = 0;
                inter = inter + (startingInter * 2);
            }

            foreach (int coordY in FrequOflastTry)
            {
                foreach (double key in intervals.Keys)
                {
                    if (coordY >= key - startingInter && coordY < key + startingInter)
                    {
                        intervals[key] += 1;
                        break;
                    }
                }
            }

            int max = 0;

            foreach (double key in intervals.Keys)
            {
                max += intervals[key];
            }

            //intervals[key] / max = x / width 
            //x = intervals[key] * width / max 

            //create the rectangles
            int numberofinterval = 0;
            foreach (double key in intervals.Keys)
            {
                Rectangle VirtualWindow1 = new Rectangle(0, 5 * numberofinterval, intervals[key] * this.bitmapHistogram.Width / max, (int)startingInter * 2);
                numberofinterval++;
                graphicsHistogram.DrawRectangle(Pens.Black, VirtualWindow1);
                //graphicsHistogram.FillRectangle(Brushes.Orange, VirtualWindow1);
            }


            this.pictureBox2.Image = bitmapHistogram;

        }

        private void Compute_Click(object sender, EventArgs e)
        {
            this.richTextBox3.Clear();
            FrequOflastTry = new List<int>();
            computeSqeuences();
            doHistogram();
        
        
        }
    }
}
