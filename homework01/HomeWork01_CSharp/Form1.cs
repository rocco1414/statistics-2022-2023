using System;
using System.Windows.Forms;

namespace HomeWork01_CSharp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "username" && txtpassword.Text == "password")
            {
                new Form2().Show();
                this.Hide();
            } else
            {
                MessageBox.Show("The User name or password you entered is incorrect, try again!");
                txtUsername.Clear();
                txtpassword.Clear();
                txtUsername.Focus();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            txtUsername.Clear();
            txtpassword.Clear();
            txtUsername.Focus();
        }
    }
}
