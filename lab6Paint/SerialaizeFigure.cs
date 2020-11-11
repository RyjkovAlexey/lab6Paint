using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6Paint
{
    [Serializable]
    public abstract class SerialaizeFigure
    {
        public abstract void Draw(Graphics gr);
    }

    [Serializable]
    public class LineSerialize : SerialaizeFigure
    {
        private Color color;
        private int width;
        private Point point1;
        private Point point2;
        public LineSerialize(Color color,int width, Point point1, Point point2)
        {
            this.color = color;
            this.width = width;
            this.point1 = point1;
            this.point2 = point2;
        }

        public override void Draw(Graphics gr)
        {
            using (var pen = new Pen(color,width))
            {
                gr.DrawLine(pen,point1,point2);
            }
        }
    }

    [Serializable]
    public class RectangleSerialize : SerialaizeFigure
    {
        private Color color;
        private int width;
        private Point point1;
        private Point point2;
        public RectangleSerialize(Color color, int width, Point point1, Point point2)
        {
            this.color = color;
            this.width = width;
            this.point1 = point1;
            this.point2 = point2;
        }
        public override void Draw(Graphics gr)
        {
            Pen pen = new Pen(new SolidBrush(color));
            gr.DrawRectangle(pen,
                (point1.X < point2.X) ? point1.X : point2.X,
                (point1.Y < point2.Y) ? point1.Y : point2.Y,
                (point2.X > point1.X) ? point2.X - point1.X : point1.X - point2.X,
                (point2.Y > point1.Y) ? point2.Y - point1.Y : point1.Y - point2.Y
            );
        }
    }

    [Serializable]
    public class EllipsSerialize : SerialaizeFigure
    {
        private Color color;
        private int width;
        private Point point1;
        private Point point2;

        public EllipsSerialize(Color color, int width, Point point1, Point point2)
        {
            this.color = color;
            this.width = width;
            this.point1 = point1;
            this.point2 = point2;
        }

        public override void Draw(Graphics gr)
        {
            Pen pen = new Pen(new SolidBrush(color));
            gr.DrawEllipse(pen,
                (point1.X < point2.X) ? point1.X : point2.X,
                (point1.Y < point2.Y) ? point1.Y : point2.Y,
                (point2.X > point1.X) ? point2.X - point1.X : point1.X - point2.X,
                (point2.Y > point1.Y) ? point2.Y - point1.Y : point1.Y - point2.Y
            );
        }
    }

    [Serializable]
    public class BrushSerialize : SerialaizeFigure
    {
        private Color color;
        private int width;
        private Point point1;

        public BrushSerialize (Color color, int width, Point point1)
        {
            this.color = color;
            this.width = width;
            this.point1 = point1;
        }

        public override void Draw(Graphics gr)
        {
            gr.FillEllipse(new SolidBrush(color), point1.X, point1.Y, width, width);
        }
    }
}
