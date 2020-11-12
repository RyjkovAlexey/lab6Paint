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
        private Draw figure;
        List<Draw> figures;
        private string selectedFigure;
        private bool isDown = false;
        private BinaryFormatter formatter = new BinaryFormatter();

        //gr.DrawRectangle(pen,
        //(pos1.X<pos2.X) ? pos1.X : pos2.X,
        //(pos1.Y<pos2.Y) ? pos1.Y : pos2.Y,
        //(pos2.X > pos1.X) ? pos2.X - pos1.X : pos1.X - pos2.X,
        //(pos2.Y > pos1.Y) ? pos2.Y - pos1.Y : pos1.Y - pos2.Y
        //);
        public Form1()
        {
            InitializeComponent();
            gr = pictureBox1.CreateGraphics();
            width = 1;
            colorBrush = pictureBox1.BackColor;
            brush = createBrush(colorBrush);
            pen = createPen(brush, width);
            selectedFigure = "brush";
            figures = new List<Draw>();
        }

        public Pen createPen(Brush br, int width)
        {
            return new Pen(br, width);
        }

        public Brush createBrush(Color color)
        {
            return new SolidBrush(color);
        }

        public void drawRect(Pen pen, Point pos1, Point pos2, Graphics gr)
        {
            gr.DrawRectangle(pen,
                (pos1.X < pos2.X) ? pos1.X : pos2.X,
                (pos1.Y < pos2.Y) ? pos1.Y : pos2.Y,
                (pos2.X > pos1.X) ? pos2.X - pos1.X : pos1.X - pos2.X,
                (pos2.Y > pos1.Y) ? pos2.Y - pos1.Y : pos1.Y - pos2.Y
            );
        }

        public void drawEllips(Pen pen, Point pos1, Point pos2, Graphics gr)
        {
            gr.DrawEllipse(pen,
                (pos1.X < pos2.X) ? pos1.X : pos2.X,
                (pos1.Y < pos2.Y) ? pos1.Y : pos2.Y,
                (pos2.X > pos1.X) ? pos2.X - pos1.X : pos1.X - pos2.X,
                (pos2.Y > pos1.Y) ? pos2.Y - pos1.Y : pos1.Y - pos2.Y);
        }

        public void drawLine(Pen pen, Point pos1, Point pos2, Graphics gr)
        {
            gr.DrawLine(pen, pos1, pos2);
        }

        public void drawBrush(Brush brush, Point pos, int size, Graphics gr)
        {
            gr.FillEllipse(brush, pos1.X, pos1.Y, size, size);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            isDown = true;
            pos1 = e.Location;
            if (selectedFigure.Equals("brush"))
            {
                figure = new DrawBrush(brush,pnlColor.BackColor,pos1,width,drawBrush);
                figure.Drawing(gr);
                figures.Add(figure);
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isDown = false;
            if (!selectedFigure.Equals("brush"))
            {
                pos2 = e.Location;
                if (selectedFigure.Equals("rectangle"))
                {
                    figure = new DrawFigure(pen,pos1,pos2,drawRect);
                    figure.Drawing(gr);
                    figures.Add(figure);
                }
                if (selectedFigure.Equals("ellips"))
                {
                    figure = new DrawFigure(pen, pos1, pos2, drawEllips);
                    figure.Drawing(gr);
                    figures.Add(figure);
                }

                if (selectedFigure.Equals("line"))
                {
                    figure = new DrawFigure(pen, pos1, pos2, drawLine);
                    figure.Drawing(gr);
                    figures.Add(figure);
                }
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
            selectedFigure = "rectangle";
        }

        private void btnEllips_Click(object sender, EventArgs e)
        {
            selectedFigure = "ellips";
        }

        private void btnLine_Click(object sender, EventArgs e)
        {
            selectedFigure = "line";
        }

        private void btnBrush_Click(object sender, EventArgs e)
        {
            selectedFigure = "brush";
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDown && e.Button == MouseButtons.Left && selectedFigure.Equals("brush"))
            {
                figure = new DrawBrush(brush,pnlColor.BackColor,MousePosition,width,drawBrush);
                figure.Drawing(gr);
                figures.Add(figure);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Draw.serialize(figures,formatter);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists("picture.dat"))
            {
                using (FileStream fs = new FileStream("picture.dat",FileMode.OpenOrCreate))
                {
                    List<Draw> deserialize = (List<Draw>) formatter.Deserialize(fs);
                    deserialize.ForEach(item=>item.Drawing(gr));
                }
            }
        }
    }
}