using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace lab6Paint
{
    public partial class Form1 : Form
    {
        private Graphics gr;
        private Point pos1;
        private Point pos2;
        private Pen pen;
        private Brush brush;
        private Color colorBrush;
        private int width;
        private Figure figure = new Figure();
        private string selectedFigure = "";
        private bool isDown = false;
        private BinaryFormatter formatter = new BinaryFormatter();
        private List<ShapeSerializer> serialaizeFigures = new List<ShapeSerializer>();

        public Form1()
        {
            InitializeComponent();
            gr = pictureBox1.CreateGraphics();
            width = 1;
            colorBrush = pictureBox1.BackColor;
            brush = createBrush(colorBrush);
            pen = createPen(brush, width);
            figure.Drawer = drawBrush;
            selectedFigure = "brush";
        }

        public Pen createPen(Brush br, int width)
        {
            return new Pen(br, width);
        }

        public Brush createBrush(Color color)
        {
            return new SolidBrush(color);
        }

        public void drawRect(Point pos1, Point pos2, Graphics gr)
        {
            gr.DrawRectangle(pen,
                (pos1.X < pos2.X) ? pos1.X : pos2.X,
                (pos1.Y < pos2.Y) ? pos1.Y : pos2.Y,
                (pos2.X > pos1.X) ? pos2.X - pos1.X : pos1.X - pos2.X,
                (pos2.Y > pos1.Y) ? pos2.Y - pos1.Y : pos1.Y - pos2.Y
            );
            serialaizeFigures.Add(new RectangleSerialize(pos1, pos2, colorBrush, width));
        }

        public void drawEllips(Point pos1, Point pos2, Graphics gr)
        {
            gr.DrawEllipse(pen,
                (pos1.X < pos2.X) ? pos1.X : pos2.X,
                (pos1.Y < pos2.Y) ? pos1.Y : pos2.Y,
                (pos2.X > pos1.X) ? pos2.X - pos1.X : pos1.X - pos2.X,
                (pos2.Y > pos1.Y) ? pos2.Y - pos1.Y : pos1.Y - pos2.Y);
            serialaizeFigures.Add(new EllipsSerialize(pos1, pos2, colorBrush, width));
        }

        public void drawLine(Point pos1, Point pos2, Graphics gr)
        {
            gr.DrawLine(pen, pos1, pos2);
            serialaizeFigures.Add(new LineSerialize(pos1, pos2, colorBrush, width));
        }

        public void drawBrush(Point pos1, Point pos2, Graphics gr)
        {
            gr.FillRectangle(brush, pos1.X, pos1.Y, pos2.X, pos2.Y);
            serialaizeFigures.Add(new BrushSerialize(pos1, width, colorBrush));
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            isDown = true;
            pos1 = e.Location;
            if (selectedFigure.Equals("brush"))
            {
                pos2.X = width;
                pos2.Y = width;
                figure.Draw(pos1, pos2, gr);
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isDown = false;
            if (!selectedFigure.Equals("brush"))
            {
                pos2 = e.Location;
                figure.Draw(pos1, pos2, gr);
            }
        }

        private void pnlColor_Click(object sender, EventArgs e)
        {
            colorBrush = (colorDialog1.ShowDialog() == DialogResult.OK) ? colorDialog1.Color : pnlColor.BackColor;
            pnlColor.BackColor = colorBrush;
            brush = createBrush(colorBrush);
            pen = createPen(brush, width);
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            width = trackBar1.Value;
            pen.Width = width;
        }

        private void btnRectangle_Click(object sender, EventArgs e)
        {
            figure.Drawer = drawRect;
            selectedFigure = "rectangle";
        }

        private void btnEllips_Click(object sender, EventArgs e)
        {
            figure.Drawer = drawEllips;
            selectedFigure = "ellips";
        }

        private void btnLine_Click(object sender, EventArgs e)
        {
            figure.Drawer = drawLine;
            selectedFigure = "line";
        }

        private void btnBrush_Click(object sender, EventArgs e)
        {
            figure.Drawer = drawBrush;
            selectedFigure = "brush";
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDown && e.Button == MouseButtons.Left && selectedFigure.Equals("brush"))
            {
                pos2 = new Point(width, width);
                figure.Draw(e.Location, pos2, gr);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (FileStream fs = new FileStream("picture.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, serialaizeFigures);
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (File.Exists("picture.dat"))
            {
                using (FileStream fs = new FileStream("picture.dat", FileMode.OpenOrCreate))
                {
                    var list = (List<ShapeSerializer>) formatter.Deserialize(fs);
                    list.ForEach(item =>
                    {
                        item.Deserialize(gr);
                        serialaizeFigures.Add(item);
                    });
                }
            }
        }
    }
}