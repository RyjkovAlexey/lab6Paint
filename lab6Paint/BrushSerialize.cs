using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6Paint
{
    [Serializable]
    class BrushSerialize : ShapeSerializer
    {
        private Point point;
        private int width;
        private Color brushColor;

        public BrushSerialize(Point point, int width, Color brushColor)
        {
            this.point = point;
            this.width = width;
            this.brushColor = brushColor;
        }

        public override void Deserialize(Graphics graphics)
        {
            graphics.FillEllipse(new SolidBrush(brushColor), point.X, point.Y, width, width);
        }
    }
}