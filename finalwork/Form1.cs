using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace finalwork
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            
            Graphics g =e.Graphics;
            g.Clear(Color.White);
            Pen pen1 = new Pen(Color.Black, 2);
            pen1.Width = 1;
            g.DrawRectangle(pen1, 10, 10, 560, 560);
            for (int i = 10; i < 560; i += 20)
            {
                g.DrawLine(pen1, i, 10, i, 570);
            }
            for (int i = 10; i < 560; i += 20)
            {
                g.DrawLine(pen1, 10, i, 570, i);
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            int x, y;
            x=(int)((e.Y-10+16/2)/16);
            y = (int)((e.X - 10 + 16 / 2) / 16);
            int a = y * 16 + 10 - 16 / 2;
            int b = x * 16 + 10 - 16 / 2;
            Console.WriteLine(x+" "+y + " " + a + " " + b + " " +e.X + " " + e.Y);
            Graphics g = pictureBox1.CreateGraphics();
            Image image = Image.FromFile(@"D:\黑棋.gif");
            g.DrawImage(image, a, b,image.Width,image.Height);
        }
    }
}
