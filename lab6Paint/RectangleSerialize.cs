using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6Paint
{
    [Serializable]
    class RectangleSerialize : ShapeSerializer
    {
        private int x, y, wight, height, size;
        private Color colorBrush;

        public RectangleSerialize(int x, int y, int wight, int height, int size, Color colorBrush)
        {
            this.x = x;
            this.y = y;
            this.wight = wight;
            this.height = height;
            this.size = size;
            this.colorBrush = colorBrush;
        }

        public override void Deserialize(Graphics graphics)
        {
            graphics.DrawRectangle(new Pen(new SolidBrush(colorBrush), this.size),
                this.x, this.y, this.wight, this.height);
        }
    }
}