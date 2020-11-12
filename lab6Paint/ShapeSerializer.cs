using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6Paint
{
    [Serializable]
    abstract class ShapeSerializer
    {
        private Point point1;
        private Point point2;
        private int width;
        private Color colorBrush;

        public abstract void Deserialize(Graphics gr);
    }
}