using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6Paint
{
    [Serializable]
    class DrawFigure : Draw
    {
        internal delegate void Drawer(Pen pen, Point pos1, Point pos2, Graphics gr = null);
        private Drawer drawer;
        private Point pos1, pos2;
        private Color color;
        private int width;
        [NonSerialized]
        private Pen pen;
        


        public DrawFigure(Pen pen, Point pos1, Point pos2, Drawer drawer)
        {
            this.pen = pen;
            this.pos1 = pos1;
            this.pos2 = pos2;
            this.drawer = drawer;
            this.color = pen.Color;
            this.width = (int)pen.Width;
        }

        public override void Drawing(Graphics gr)
        {
            if (pen!=null)
            {
                drawer(pen, pos1, pos2, gr);
            }
            else
            {
                drawer(new Pen(new SolidBrush(color), width), pos1, pos2, gr);
            }
        }
    }
}
