using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using Microsoft.VisualBasic.FileIO;

namespace homework06
{
    public partial class Form1 : Form
    {
        TextFieldParser Parser = null;
        List<Packet> packets = null;

        Rectangle r1;
        Rectangle r2;
        Bitmap b;
        Graphics g;

        int sizeOfSample = 10;
        int numbersOfSample = 100;

        Random r = new Random();

        double[][] samples;

        double[] means;
        double[] variances;

        SortedDictionary<Interval, int> meanDistribution;
        SortedDictionary<Interval, int> varianceDistribution;

        public Form1()
        {
            InitializeComponent();
            computeDistribution();
            initGraphics();
        }

        private void initGraphics()
        {
            b = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(b);

            r1 = new Rectangle(5, 5, pictureBox1.Width / 2, pictureBox1.Height / 2);
            r2 = new Rectangle((pictureBox1.Width / 2) + 10, 10, pictureBox1.Width / 2, pictureBox1.Height / 2);
        }
        private void computeDistribution()
        {
            if (Parser != null)
            {
                Parser.Close();
            }
            
            string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            this.richTextBox1.AppendText(sCurrentDirectory);
            string sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\wireshark.csv");
            string sFilePath = Path.GetFullPath(sFile);
            Parser = new TextFieldParser(sFilePath);
            Parser.Delimiters = new string[] { "," };
            Parser.ReadFields();
            packets = new List<Packet>();
            while (!Parser.EndOfData)
            {
                string[] currentrow = Parser.ReadFields();

                for (int i = 0; i < currentrow.Length; i++)
                {
                    currentrow[i] = currentrow[i].Trim('"');
                }

                int no = Convert.ToInt32(currentrow[0]);
                double t = Convert.ToDouble(currentrow[1]);
                string sa = Convert.ToString(currentrow[2]);
                string da = Convert.ToString(currentrow[3]);
                string p = Convert.ToString(currentrow[4]);
                int len = Convert.ToInt32(currentrow[5]);
                string inf = Convert.ToString(currentrow[6]);

                Packet pack = new Packet(no, t, sa, da, p, len, inf);
                packets.Add(pack);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            timer1.Stop();
            sizeOfSample = (int)numericUpDown2.Value;
            numbersOfSample = (int)numericUpDown1.Value;

            samples = new double[numbersOfSample][];

            for (int i = 0; i < numbersOfSample; i++)
            {
                double[] sample = new double[sizeOfSample];

                for (int j = 0; j < sizeOfSample; j++)
                {
                    int ri = r.Next(0, packets.Count);
                    Packet p = packets[ri];

                    sample[j] = (double)p.length;
                }
                samples[i] = sample;
            }

            // compute the means
            // compute the variances
            means = new double[numbersOfSample];
            variances = new double[numbersOfSample];
            for (int h = 0; h < numbersOfSample; h++)
            {
                means[h] = getMean(samples[h]);
                variances[h] = getVariance(samples[h]);
            }

            // compute mean distribution
            getMeanDistribution();

            //compute variance distribution
            getVarianceDistribution();

            timer1.Start();

            //expected value of the distribution vs expected value of the means
            List<double> all = new List<double>();
            foreach (Packet p in packets)
            {
                all.Add(p.length);
            }
            double[] all_sizes = all.ToArray();

            double expectedValueOfPopulation = getMean(all_sizes);
            double expectedValueOfSampleMean = getMean(means);
            double expectedValueOfSampleVariance = getMean(variances);

            double varianceOfPopulation = getVariance(all_sizes);
            double varianceOfSampleMean = getVariance(means);
            double VarianceOfSampleVariance = getVariance(variances);

            richTextBox1.AppendText("".PadRight(45) + "Mean".PadRight(20) + "Variance".PadRight(25) + "\n");
            richTextBox1.AppendText("Entire population:" + "".PadRight(17) + Math.Round(expectedValueOfPopulation, 2).ToString().PadRight(20) + Math.Round(varianceOfPopulation, 2).ToString() + "\n");
            richTextBox1.AppendText("Sample mean:" + "".PadRight(22) + Math.Round(expectedValueOfSampleMean, 2).ToString().PadRight(20) + Math.Round(varianceOfSampleMean, 2).ToString() + "\n");
            richTextBox1.AppendText("Sample variance:" + "".PadRight(16) + Math.Round(expectedValueOfSampleVariance, 2).ToString().PadRight(18) + Math.Round(VarianceOfSampleVariance, 2).ToString() + "\n");
        }

        public double getMean(double[] numbers)
        {
            int count = 1;
            double mean = 0;
            int size = numbers.Length;
            for (int i = 0; i < size; i++)
            {
                mean = mean + ((numbers[i] - mean) / (double)count);
                count++;
            }
            return mean;
        }

        public double getVariance(double[] n)
        {
            double mean = 0;
            double previousMean;
            double variance = 0;
            int count = 1;
            int size = n.Length;

            for (int j = 0; j < size; j++)
            {
                double val = n[j];
                previousMean = mean;
                mean = mean + (val - mean) / count;
                variance = variance + (val - mean) * (val - previousMean);
                count++;
            }
            variance = variance / (n.Length);
            return (variance);
        }

        public void getMeanDistribution()
        {
            double minMean = means.Min();
            int min = (int)Math.Floor(minMean);

            int sizeInterval = 20;
            int next = min + sizeInterval;

            meanDistribution = new SortedDictionary<Interval, int>();

            Interval inter = new Interval(min, next);
            meanDistribution[inter] = 0;

            for (int h = 0; h < means.Length; h++)
            {
                bool inserted = false;

                List<Interval> keys = meanDistribution.Keys.ToList();
                foreach (Interval v in keys)
                {
                    if (means[h] >= v.down && means[h] < v.up)
                    {
                        inserted = true;
                        meanDistribution[v]++;
                    }
                }
                while (!inserted)
                {
                    Interval newInterval = new Interval(next, next + sizeInterval);
                    next = next + sizeInterval;
                    meanDistribution[newInterval] = 0;

                    if (means[h] >= newInterval.down && means[h] < newInterval.up)
                    {
                        meanDistribution[newInterval]++;
                        inserted = true;
                    }
                }
            }
        }

        public void getVarianceDistribution()
        {
            double minVariance = variances.Min();
            int minV = (int)Math.Floor(minVariance);

            int intervalSize = 10000;
            int next = minV + intervalSize;

            varianceDistribution = new SortedDictionary<Interval, int>();

            Interval interval = new Interval(minV, next);
            varianceDistribution[interval] = 0;

            for (int k = 0; k < variances.Length; k++)
            {
                bool inserted = false;

                List<Interval> keys = varianceDistribution.Keys.ToList();
                foreach (Interval v in keys)
                {
                    if (variances[k] >= v.down && variances[k] < v.up)
                    {
                        inserted = true;
                        varianceDistribution[v]++;
                    }
                }
                while (!inserted)
                {
                    Interval newInterval = new Interval(next, next + intervalSize);
                    next = next + intervalSize;
                    varianceDistribution[newInterval] = 0;

                    if (variances[k] >= newInterval.down && variances[k] < newInterval.up)
                    {
                        varianceDistribution[newInterval]++;
                        inserted = true;
                    }
                }
            }
        }

        
        private void drawHistogram_mean()
        {

            g.FillRectangle(Brushes.White, r1);
            g.DrawRectangle(Pens.Black, r1);

            int maxvalue = meanDistribution.Values.Max();

            int height = r1.Bottom - r1.Top - 20;
            int width = r1.Right - r1.Left - 20;

            int numberOfInterval = meanDistribution.Keys.Count;
            int histrect_width = width / numberOfInterval;

            int start = r1.X;

            foreach (KeyValuePair<Interval, int> k in meanDistribution)
            {
                int heightRectangle = (int)(((double)k.Value / (double)maxvalue) * ((double)height));
                Rectangle histogramRectangle = new Rectangle(start, r1.Bottom - heightRectangle, histrect_width, heightRectangle);

                g.FillRectangle(Brushes.Orange, histogramRectangle);
                g.DrawRectangle(Pens.White, histogramRectangle);

                Interval intervalKey = k.Key;
                string text = intervalKey.ToString();
                Rectangle stringPos = new Rectangle(start, r1.Bottom + 20, histrect_width, histrect_width + 20);
                Font font1 = new Font("Arial", 9, FontStyle.Regular, GraphicsUnit.Pixel);

                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                Font goodFont = findFont(g, text, stringPos.Size, font1);

                g.DrawString(text, goodFont, Brushes.Black, stringPos, stringFormat);

                start += histrect_width;
            }
        }
        private void drawHistogram_variance()
        {
            g.FillRectangle(Brushes.White, r2);
            g.DrawRectangle(Pens.Black, r2);

            int maxvalue = varianceDistribution.Values.Max();

            int height = r2.Bottom - r2.Top - 20;
            int width = r2.Right - r2.Left - 20;

            int numberOfInterval = varianceDistribution.Keys.Count;
            int histrect_width = width / numberOfInterval;

            int start = r2.X;

            foreach (KeyValuePair<Interval, int> k in varianceDistribution)
            {
                int heightRectangle = (int)(((double)k.Value / (double)maxvalue) * ((double)height));
                Rectangle histogramRectangel = new Rectangle(start, r2.Bottom - heightRectangle, histrect_width, heightRectangle);

                g.FillRectangle(Brushes.Blue, histogramRectangel);
                g.DrawRectangle(Pens.White, histogramRectangel);

                Interval interval_k = k.Key;
                string text = interval_k.ToString();
                Rectangle stringPos = new Rectangle(start, r2.Bottom + 20, histrect_width, histrect_width + 20);
                Font font1 = new Font("Arial", 18, FontStyle.Regular, GraphicsUnit.Pixel);

                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                Font goodFont = findFont(g, text, stringPos.Size, font1);

                g.DrawString(text, goodFont, Brushes.Black, stringPos, stringFormat);


                start += histrect_width;
            }
        }

        private Font findFont(Graphics g, string myString, Size Room, Font PreferedFont)
        {
            SizeF RealSize = g.MeasureString(myString, PreferedFont);
            float HeightScaleRatio = Room.Height / RealSize.Height;
            float WidthScaleRatio = Room.Width / RealSize.Width;

            float ScaleRatio = (HeightScaleRatio < WidthScaleRatio) ? ScaleRatio = HeightScaleRatio : ScaleRatio = WidthScaleRatio;

            float ScaleFontSize = PreferedFont.Size * ScaleRatio;

            if (ScaleFontSize <= 0)
            {
                ScaleFontSize = 0.0000001f;
            }

            return new Font(PreferedFont.FontFamily, ScaleFontSize);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            g.Clear(pictureBox1.BackColor);
            drawHistogram_mean();
            drawHistogram_variance();
            pictureBox1.Image = b;
        }
    }
}
