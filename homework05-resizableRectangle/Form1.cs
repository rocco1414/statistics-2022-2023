using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace homework05_resizableRectangle
{
    public partial class Form1 : Form
    {

        public Bitmap b;
        public Graphics g;
        public Font smallFont = new Font("Calibri", 10, FontStyle.Regular, GraphicsUnit.Pixel);
        public List<DataPoint> dataPoints = new List<DataPoint>();

        //World Window (in questo caso mi prende tutti i quadranti)
        //se setto minX e minY a 0, posiziono la window solo sul quadrante in alto a sinistra
        //quindi posso decidere quali punti inserire nella viewport.
        private double MinX_window;
        private double MaxX_window;
        private double MinY_window;
        private double MaxY_window;
        private double RangeX;
        private double RangeY;
        //----------------------------------------------------------------

        //VIEWPORT
        public Rectangle viewPort = new Rectangle(400, 50, 500, 500);
        //----------------------------------------------------------------

        public void InitialGraphics()
        {
            this.b = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            g = Graphics.FromImage(this.b);
            this.g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            this.g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            MinX_window = -250;
            MaxX_window = 250;
            MinY_window = -250;
            MaxY_window = 250;
            RangeX = MaxX_window - MinX_window;
            RangeY = MaxY_window - MinY_window;

        }

        public Form1()
        {
            InitializeComponent();
            this.InitialGraphics();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataSet dataSet = new DataSet();

            for (int i = -5; i <= 5; i++)
            {
                for (int j = -5; j <= 5; j++)
                {
                    DataPoint dataPoint = new DataPoint();
                    dataPoint.SetX(i * 50);
                    dataPoint.SetY(j * 50);
                    dataSet.GetDataPoints().Add(dataPoint);
                }
            }

            //--------------- WITHOUT DO ANY TRANSOFRM ------------------

            //this.richTextBox2.Clear();
            dataPoints = dataSet.GetDataPoints();
            //foreach (DataPoint data in dataPoints)
            //{
            //    this.richTextBox2.AppendText("\n");
            //    this.richTextBox2.AppendText(data.GetX().ToString().PadRight(10) + data.GetY().ToString().PadRight(10));
            //}

            this.drawScene();
        }

        private void drawScene()
        {
            //Clear the scene
            g.Clear(Color.White);

            //Draw the vieport
            this.g.DrawRectangle(Pens.DarkBlue, viewPort);

            //draw the point
            foreach (DataPoint data in dataPoints)
            {
                //WITHOUT TRANSFORMATION
                //this.g.FillEllipse(Brushes.Blue, new Rectangle(new Point(((int)data.GetX()) - 3, (int)data.GetY() - 3), new Size(6,6)));
                //g.DrawString(data.GetX().ToString() + "," + data.GetY().ToString(), smallFont, Brushes.Red, new Point((int)data.GetX(), (int)data.GetY()));

                //WITH MANUAL TRANDSOFRMATION
                int X_Device = this.X_ViewPort(data.GetX(), viewPort, MinX_window, RangeX);
                int Y_Device = this.Y_ViewPort(data.GetY(), viewPort, MinY_window, RangeY);

                if (this.viewPort.Contains(X_Device, Y_Device))
                {
                    this.g.FillEllipse(Brushes.Salmon, new Rectangle(new Point((X_Device) - 3, Y_Device - 3), new Size(6, 6)));
                    g.DrawString(data.GetX().ToString() + "," + data.GetY().ToString(), smallFont, Brushes.Black, new Point(X_Device, Y_Device));

                }
            }

            this.pictureBox1.Image = b;

        }

        // -------------------- DOING MANUAL TRANSOFRM --------------------

        private int X_ViewPort(double X_world, Rectangle viewPort, double minX_window, double rangeX)
        {
            return (int)(viewPort.Left + ((X_world - minX_window) * viewPort.Width) / rangeX);
        }


        private int Y_ViewPort(double Y_world, Rectangle viewPort, double min_Y, double rangeY)
        {
            return (int)(viewPort.Top + viewPort.Height - ((Y_world - min_Y) * viewPort.Height) / rangeY);
        }

        //--------------------------------------------------------------------------------

        private void Form1_Load(object sender, EventArgs e)
        {
            Application.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
        }

        // How to move, resize and zooming viewport using the mause.
        //In the mouse douw we save the current position of mouse and of the viewpot
        //if there is MouseMouve we compute the shift in the X and Y direction, this delta will be added to the position of viewport

        //Event of picturebox
        private Rectangle viewportAtMouseDown;
        private Point mouseLocationAtMouseDown;
        private bool draggingStarted = false;
        private bool resizingStarted = false;

        private double MinXWindowAtMouseDown;
        private double MaxXWindowAtMouseDown;
        private double MinYWindowAtMouseDown;
        private double MaxYWindowAtMouseDown;
        private double RangeXAtMouseDown;
        private double RangeYAtMouseDown;

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            this.pictureBox1.Focus();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            this.draggingStarted = false;
            this.resizingStarted = false;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.viewPort.Contains(e.X, e.Y))
            {
                // Mi salvo la posizione del mouse e la viewport nel momento in cui clicclo il mouse
                this.viewportAtMouseDown = this.viewPort;
                this.mouseLocationAtMouseDown = new Point(e.X, e.Y);

                this.MinXWindowAtMouseDown = MinX_window;
                this.MaxXWindowAtMouseDown = MaxX_window;
                this.MinYWindowAtMouseDown = MinY_window;
                this.MaxYWindowAtMouseDown = MaxY_window;
                this.RangeXAtMouseDown = RangeX;
                this.RangeYAtMouseDown = RangeY;
                //Se premo il tasto sinistro --> mi muovo
                //Se premo il tasto destro --> resizing
                if (e.Button == MouseButtons.Left)
                {
                    this.draggingStarted = true;
                }
                else
                {
                    this.resizingStarted = true;
                }
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            //questo va fatto solo se prima c'è stato un evento mouseDown
            // e solo se il mouse è all'interno della viewport

            //sappiamo di quanto si è spostato il mouse
            int Delta_x = e.X - this.mouseLocationAtMouseDown.X;
            int Delta_y = e.Y - this.mouseLocationAtMouseDown.Y;

            if (this.draggingStarted)
            {
                if (ModifierKeys.HasFlag(Keys.Control))
                {
                    double realWorldDeltaX = this.RangeXAtMouseDown * Delta_x / this.viewPort.Width;
                    MinX_window = MinXWindowAtMouseDown - realWorldDeltaX;
                    MaxX_window = MaxXWindowAtMouseDown - realWorldDeltaX;

                    double realWorldDeltaY = this.RangeYAtMouseDown * Delta_y / this.viewPort.Height;
                    MinY_window = MinYWindowAtMouseDown + realWorldDeltaY;
                    MaxY_window = MaxYWindowAtMouseDown + realWorldDeltaY;

                }
                else
                {
                    //dobbiamo spostare la viewport della stessa quantita'
                    this.viewPort.X = this.viewportAtMouseDown.X + Delta_x;
                    this.viewPort.Y = this.viewportAtMouseDown.Y + Delta_y;
                }

            }
            else if (this.resizingStarted)
            {
                if (ModifierKeys.HasFlag(Keys.Control))
                {
                    double realWorldDeltaX = this.RangeXAtMouseDown * Delta_x / this.viewPort.Width;
                    this.MaxX_window = MaxXWindowAtMouseDown - realWorldDeltaX;
                    this.RangeX = this.RangeXAtMouseDown - realWorldDeltaX;

                    double realWorldDeltaY = this.RangeYAtMouseDown * Delta_y / this.viewPort.Height;
                    this.MaxY_window = MaxYWindowAtMouseDown + realWorldDeltaY;
                    this.RangeY = this.RangeYAtMouseDown + realWorldDeltaY;
                }
                else
                {
                    //facciamo il resize della viewport
                    this.viewPort.Width = this.viewportAtMouseDown.Width + Delta_x;
                    this.viewPort.Height = this.viewportAtMouseDown.Height + Delta_y;
                }

            }

            //Update of the drawing
            this.drawScene();
        }





        ////Zoom
        private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            int changeX = 10; //(int) this.viewPort.Width /10'
            int changeY = (int)this.viewPort.Height * changeX / this.viewPort.Width;

            if (ModifierKeys.HasFlag(Keys.Control))
            {
                double realWorldDeltaX = this.RangeXAtMouseDown * changeX / this.viewPort.Width;
                double realWorldDeltaY = this.RangeYAtMouseDown * changeY / this.viewPort.Height;
                if (e.Delta > 0)
                {
                    this.MinX_window -= realWorldDeltaX;
                    this.RangeX += 2 * realWorldDeltaX;

                    this.MinY_window -= realWorldDeltaY;
                    this.RangeY += 2 * realWorldDeltaY;
                }
                else
                {
                    this.MinX_window += realWorldDeltaX;
                    this.RangeX -= 2 * realWorldDeltaX;

                    this.MinY_window += realWorldDeltaY;
                    this.RangeY -= 2 * realWorldDeltaY;
                }
            }
            else
            {
                if (e.Delta > 0)
                {
                    this.viewPort.X -= changeX;
                    this.viewPort.Width += 2 * changeX;
                    this.viewPort.Y -= changeY;
                    this.viewPort.Height += 2 * changeY;
                }
                else
                {
                    this.viewPort.X += changeX;
                    this.viewPort.Width -= 2 * changeX;
                    this.viewPort.Y += changeY;
                    this.viewPort.Height -= 2 * changeY;
                }
            }
            this.drawScene();
        }


    }
}
