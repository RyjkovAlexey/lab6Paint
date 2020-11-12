using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab6Paint
{
    [Serializable]
    abstract class Draw
    {
        private Point pos1;

        public virtual void Drawing(Graphics gr)
        {

        }

        public static void serialize(List<Draw> list, BinaryFormatter formatter)
        {
            using (FileStream fs = new FileStream("picture.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, list);
            }
        }
    }
}