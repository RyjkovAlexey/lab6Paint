using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab6Paint
{
    public partial class Form1 : Form
    {
        private Graphics gr;
        public Form1()
        {
            InitializeComponent();
            gr = pictureBox1.CreateGraphics();
        }


        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {

        }
    }
}
