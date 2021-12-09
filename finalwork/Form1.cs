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
        Pen pen1 = null;
        private C_Num player;
        public Image[] image = {
            finalwork.Properties.Resources.标记,
            finalwork.Properties.Resources.黑棋,
            finalwork.Properties.Resources.白棋};
        public int N = 18, M = 18;//规定绘制的棋盘的行和列
        public int dis = 30, radius = 8;//规定棋格的长度和棋子的长度
        public int border = 15;//棋盘和窗体的边距长度
        private QiPan qipan;
        public Graphics g;
        public Graphics gs;
        private string[] str = { "黑方", "白方" };
        private bool[] quit;                                //标记各个玩家是否已经退出
        private int[] win;                                  //标记依次获胜的各个玩家
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            player = new C_Num(2);                        //初始化num个玩家
            qipan = new QiPan(M, N, 2);
            pen1 = new Pen(Color.Black, 2);
            pen1.Width = 1;
            g = pictureBox1.CreateGraphics();               //在对象图片框中创建一个Graphics对象用于绘图
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

            pictureBox1.Width = (N - 1) * dis + 2 * border; //根据行列数设置棋盘大小
            pictureBox1.Height = (M - 1) * dis + 2 * border;
            g = e.Graphics;
            g.Clear(Color.White);
            drawQiPan(g);
            g = pictureBox1.CreateGraphics();
            pictureBox1.Left = 0;
            pictureBox1.Top = 0;
            Height = Height - ClientSize.Height + pictureBox1.Height;
        }
        private void drawQiPan(Graphics g)
        {

            g.DrawRectangle(pen1, border, border, pictureBox1.Width - 2 * border, pictureBox1.Height - 2 * border);
            for (int i = border; i < (N - 1) * dis + border; i += dis)
            {
                g.DrawLine(pen1, i, border, i, (N - 1) * dis + border);
            }
            for (int i = border; i < (M - 1) * dis + border; i += dis)
            {
                g.DrawLine(pen1, border, i, (N - 1) * dis + border, i);
            }
        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            int X, Y;//用鼠标单击事件获取落子的坐标
            X = (int)((e.Y - border + dis / 2) / dis);      //计算在棋盘中的行坐标
            Y = (int)((e.X - border + dis / 2) / dis);      //列坐标
            //drawpic(1, X, Y);
            putChess(X, Y);
        }
        private void putChess(int x, int y)                 //玩家在棋盘上坐标(x,y)处落下棋子
        {
            label2.Text = "游戏开始";
            int preplayer;
            if (0 <= x && x < M && 0 <= y && y < N)         //下棋子的位置在棋盘之内时，在棋盘上绘制棋子
            {
                //测试区
                if (qipan.player[x, y] == 0)                 //如果该处无棋子
                {
                    qipan.put(x, y, player.getNext(1));       //下一个玩家落下棋子,在棋盘中记录该信息
                    drawpic(qipan.player[x, y], x, y);      //绘制该玩家对应的棋子
                    //drawpic(1, x, y);
                    drawpic(0, x, y);                       //在棋子上绘制标记
                    if (qipan.num > 1)                      //重新绘制下的前一颗棋子，去除标记
                    {
                        preplayer = qipan.player[qipan.X[qipan.num - 2], qipan.Y[qipan.num - 2]];
                        //drawpic(2, x, y);
                        drawpic(preplayer, qipan.X[qipan.num - 2], qipan.Y[qipan.num - 2]);
                    }

                    if (qipan.fiveNum(x, y))                //当其中一方获胜时
                    {
                        int playerN = qipan.player[x, y];
                        MessageBox.Show(str[playerN - 1] + "胜出");
                        if (DialogResult.Yes ==
                            MessageBox.Show("再来一盘？", "信息", MessageBoxButtons.YesNo))

                            label2.Text = "游戏尚未开始";
                        again();//再来一盘

                    }
                    pass();                                 //下一位玩家下棋
                }
                else
                {
                    MessageBox.Show("已有棋子");
                }
            }
        }
        private void pass()                                 //让一子 或 交出下子权给下一位玩家
        {
            if (player != null)
            {
                player.next(1);                                 //下一位玩家//Tip();                                          //提示落子
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            player.getfirst(2);
            label2.Text = "游戏尚未开始";
            qipan.clear();
            pictureBox1.Width = (N - 1) * dis + 2 * border; //根据行列数设置棋盘大小
            pictureBox1.Height = (M - 1) * dis + 2 * border;
            g.Clear(Color.White);
            drawQiPan(g);
            g = pictureBox1.CreateGraphics();
            pictureBox1.Left = 0;
            pictureBox1.Top = 0;
            Height = Height - ClientSize.Height + pictureBox1.Height;
            pass();
        }
        private void drawpic(int pic, int x, int y)         //在棋盘上坐标(x,y)处绘制下标为pic的image图像
        {
            int X = y * dis + border - dis / 2,             //计算在棋盘上的像素横坐标
                Y = x * dis + border - dis / 2;             //纵坐标
            g.DrawImage(image[pic], X, Y,
                (int)(image[pic].Width), (int)(image[pic].Height));
        }
        private void again()
        {
            player.getfirst(2);
            qipan.clear();                      //清空棋盘上的棋子
            pictureBox1.Width = (N - 1) * dis + 2 * border; //根据行列数设置棋盘大小
            pictureBox1.Height = (M - 1) * dis + 2 * border;
            g.Clear(Color.White);
            drawQiPan(g);
            g = pictureBox1.CreateGraphics();
            pictureBox1.Left = 0;
            pictureBox1.Top = 0;
            Height = Height - ClientSize.Height + pictureBox1.Height;
            pass();
        }
    }
}