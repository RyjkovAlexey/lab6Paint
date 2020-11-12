using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6Paint
{
    [Serializable]
    class LineSerialize : ShapeSerializer
    {
        private Point point1, point2;
        private Color colorBrush;
        private int width;

        public LineSerialize(Point point1, Point point2, Color colorBrush, int width)
        {
            this.point1 = point1;
            this.point2 = point2;
            this.colorBrush = colorBrush;
            this.width = width;
        }

        public override void Deserialize(Graphics graphics)
        {
            graphics.DrawLine(new Pen(new SolidBrush(colorBrush)),point1,point2);
        }
    }
}
