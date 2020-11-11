using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab6Paint
{
    class Figure
    {
        internal delegate void DrawerFigure(Point pos1, Point pos2, Graphics gr);

        private DrawerFigure drawer;

        public DrawerFigure Drawer
        {
            get => drawer;
            set => drawer = value;
        }

        public void Draw(Point pos1, Point pos2, Graphics gr)
        {
            drawer(pos1,pos2,gr);
        }
    }
}
