using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeWork01_CSharp
{
    public partial class Form2 : Form
    {
        private List<Studente> studentes;
        private Studente studente1;
        private Studente studente2;
        private Studente studente3;

        public Form2()
        {
            InitializeComponent();
            studente1 = new Studente("Rocco", 1.80);
            studente2 = new Studente("Antonio", 1.60);
            studente3 = new Studente("Giovanni", 1.70);
            studentes = new List<Studente>();
            studentes.Add(studente1);
            studentes.Add(studente2);
            studentes.Add(studente3);
        }

        
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            int numberOfStudents = studentes.Count;
            if (numberOfStudents==0)
            {
                richTextBox1.Text = "There are no students to analyze.";
            } else
            {
                double weightSum = 0.0;
                for (int i = 0; i < numberOfStudents; i++)
                {
                    weightSum += studentes[i].GetHeight();
                }
                double averageHeight = weightSum / numberOfStudents;
                richTextBox1.Text = "The students analyzed are:\n " + studente1.GetName() + "\n " + studente2.GetName() + "\n " + studente3.GetName();
                richTextBox1.AppendText("\nThe average height is: \n" + averageHeight.ToString() + " meters.");
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
        }
    }

    public class Studente
    {
        private String name;
        private Double height;

        public Studente(String current_name, Double current_height)
        {
            name = current_name;
            height = current_height;
        }

        public string GetName()
        {
            return name;
        }

        public double GetHeight()
        {
            return height;
        }
    }
}
