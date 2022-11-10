using App_Wshark_C;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WireShark_csv
{
    public partial class Form1 : Form
    {
        public List<Packet> packets = new List<Packet>();

        Dictionary<string, int> data = new Dictionary<string, int>();
        int column = 2;

        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.ShowDialog();

            if (!String.IsNullOrWhiteSpace(o.FileName))
            {
                this.richTextBox1.Text = o.FileName;
            }
            else
            {
                MessageBox.Show("Select a valid file!", "Save error", MessageBoxButtons.OK);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String path = this.richTextBox1.Text;
            var query = from l in File.ReadAllLines(path) 
                        let data = l.Split(',')
                        select new Packet
                        {
                            No = data[0].Trim('"'),
                            Time = data[1].Trim('"'),
                            Source = data[2].Trim('"'),
                            Destination = data[3].Trim('"'),
                            Protocol = data[4].Trim('"'),
                            Length = data[5].Trim('"'),
                            Info = data[6].Trim('"'),
                        };
            Packet[] pack = query.ToArray();
            packets = pack.ToList();
            dataGridView1.DataSource = packets;
        }

        private int i = 0;
        private void button3_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Clear();
            if (packets.Count == 0)
            {
                this.richTextBox1.AppendText("Take a file and read it! ");
            }

            List<string> allvalues = new List<string>();
            
            foreach (Packet packet in packets)
            {
                string protocol = packet.Protocol;
                if (i != 0)
                {
                    if (!allvalues.Contains(protocol))
                    {
                        allvalues.Add(protocol);
                    }
                }
                i++;
            }
            i = 0;

            string[] allvaluesArr = allvalues.ToArray();
            int[] times = new int[allvaluesArr.Length];

            foreach (Packet packet1 in packets)
            {
                string protocol = packet1.Protocol;
                for (int i = 0; i < times.Length; i++)
                {
                    if (allvaluesArr[i] == protocol)
                    {
                        times[i]++;
                    }
                }
            }

            dataGridView2.Columns.Add("Protocol", "Protocol");
            dataGridView2.Columns.Add("Absolute Frequency", "Absolute Frequency");
            dataGridView2.Columns.Add("Relative Frequency", "Relative Frequency");

            for (int i = 0; i < times.Length; i++)
            {
                double relative = (double)times[i] / (double)packets.Count;

                string[] row = new string[] { allvaluesArr[i], times[i].ToString(), relative.ToString() };
                dataGridView2.Rows.Add(row);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Clear();
            if (packets.Count == 0)
            {
                this.richTextBox1.AppendText("Take a file and read it! ");
            }

            Dictionary<Interval, int> dict = new Dictionary<Interval, int>();
            int max_value = 64;
            Interval i_0 = new Interval(0, max_value);

            dict[i_0] = 0;

            foreach (Packet packet in packets)
            {
                if(i == 0)
                {
                    i++;    
                } else
                {
                    bool inserted = false;
                    List<Interval> list = dict.Keys.ToList();
                    this.richTextBox1.AppendText(packet.Length.GetType() + "\n");
                    int lunghezza = int.Parse(packet.Length.Trim());

                    foreach (Interval i in list)
                    {
                        if (lunghezza >= i.down && lunghezza <= i.up)
                        {
                            dict[i]++;
                            inserted = true;
                            break;
                        }

                    }
                    while (!inserted)
                    {
                        Interval i_x = new Interval(max_value + 1, max_value * 2);
                        max_value = max_value * 2;

                        dict[i_x] = 0;

                        if (lunghezza >= i_x.down && lunghezza <= i_x.up)
                        {
                            dict[i_x]++;
                            inserted = true;
                        }
                    }
                }
                
            }

            dataGridView3.Columns.Add("Size", "Size");
            dataGridView3.Columns.Add("Absolute Frequency", "Absolute Frequency");
            dataGridView3.Columns.Add("Relative Frequency", "Relative Frequency");

            foreach (KeyValuePair<Interval, int> item in dict)
            {
                Interval inter = item.Key;
                int absfreq = item.Value;
                double relfreq = ((double)absfreq / (double)packets.Count);

                string s1 = inter.ToString();
                string s2 = absfreq.ToString();
                string s3 = relfreq.ToString();

                string[] row = new string[] { s1, s2, s3 };

                dataGridView3.Rows.Add(row);
            }
        }
    }
}
