using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6Paint
{
    [Serializable]
    class DrawBrush : Draw
    {
        internal delegate void Drawer(Brush brush, Point pos, int size, Graphics gr = null);
        private Point pos;
        private int size;
        private Color color;
        [NonSerialized]
        private Brush brush;
        private Drawer drawer;

        public DrawBrush(Brush brush,Color colorBrush, Point pos, int size, Drawer drawer)
        {
            this.brush = brush;
            this.pos = pos;
            this.size = size;
            this.drawer = drawer;
            this.color = colorBrush;
        }


        public override void Drawing(Graphics gr)
        {
            if (brush!=null)
            {
                drawer(brush, pos, size, gr);
            }
            else
            {
                drawer(new SolidBrush(this.color), pos, size, gr);
            }
        }
    }
}
