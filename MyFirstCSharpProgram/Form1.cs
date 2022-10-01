using System;
using System.Windows.Forms;

namespace MyFirstCSharpProgram
{
    public partial class MyProgram : Form
    {
        public MyProgram()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.button.Text = "Next";
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            this.button.Text = "Enter";
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
