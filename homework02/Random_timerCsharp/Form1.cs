using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Random_timerCsharp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public Random r = new Random();

        private void button2_Click(object sender, EventArgs e)
        {
            this.timer1.Stop();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.timer1.Start();
        }

        public int i;
        public double CurrentArithmeticMean = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            i += 1;
            String nameOfExam = "Exam" + i;
            int gradeOfExam = r.Next(18, 31);
            CurrentArithmeticMean = CurrentArithmeticMean + (gradeOfExam - CurrentArithmeticMean) / i;
            this.richTextBox1.AppendText(nameOfExam.PadRight(10) + gradeOfExam + " Current Mean: " + CurrentArithmeticMean + "\n");

        }
    }
}
