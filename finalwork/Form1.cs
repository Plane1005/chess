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
        public Image[] image = {Properties.Resources.标记,
            Properties.Resources.黑棋,
            Properties.Resources.白棋};
        public int N = 18, M = 18;//规定绘制的棋盘的行和列
        public int dis = 30, radius = 8;//规定棋格的长度和棋子的长度
        public int border = 10;//棋盘和窗体的边距长度
        private QiPan qipan;
        public Graphics g;
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
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

            pictureBox1.Width = (N - 1) * dis + 2 * border; //根据行列数设置棋盘大小
            pictureBox1.Height = (M - 1) * dis + 2 * border;
            //g = pictureBox1.CreateGraphics();               //在对象图片框中创建一个Graphics对象用于绘图
            g = e.Graphics;
            g.Clear(Color.White);
            drawQiPan();
            pictureBox1.Left = 0;
            pictureBox1.Top = 0;
            Height = Height - ClientSize.Height + pictureBox1.Height;
        }
        private void drawQiPan()
        {
            g.DrawRectangle(pen1, border, border, pictureBox1.Width - 2 * border, pictureBox1.Height - 2 * border);
            for (int i = border; i < (N - 1) * dis; i += dis)
            {
                g.DrawLine(pen1, i, border, i, (N - 1) * dis + border);
            }
            for (int i = border; i < (M - 1) * dis - border; i += dis)
            {
                g.DrawLine(pen1, border, i, (N - 1) * dis + border, i);
            }
        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            //int x, y;
            // x = (int)((e.Y - 10 + 16 / 2) / 16);
            //// x = (int)(e.X - 10) / 40 * 40+10;
            // y = (int)((e.X - 10 + 16 / 2) / 16);
            // int a = y * 16 + 10 - 16 / 2;
            // int b = x * 16 + 10 - 16 / 2;
            int X, Y;
            //用鼠标单击事件获取落子的坐标
            X = (int)((e.Y - border + dis / 2) / dis);      //计算在棋盘中的行坐标
            Y = (int)((e.X - border + dis / 2) / dis);      //列坐标
            drawpic(1, X, Y);
            putChess(X, Y);


            //Console.WriteLine(x + " " + y + " " + a + " " + b + " " + e.X + " " + e.Y);
            //Graphics g = pictureBox1.CreateGraphics();
            //Image image = Image.FromFile(@"D:\黑棋.gif");
            //g.DrawImage(image, a, b, image.Width, image.Height);

        }
        private void putChess(int x, int y)                 //玩家在棋盘上坐标(x,y)处落下棋子
        {
            int preplayer;
            if (0 <= x && x < M && 0 <= y && y < N)         //下棋子的位置在棋盘之内时，在棋盘上绘制棋子
            {
                //测试区
                if (qipan.player[x, y] == 0)                 //如果该处无棋子
                {
                    qipan.put(x, y, player.getNext(1));       //下一个玩家落下棋子,在棋盘中记录该信息
                    //drawpic(qipan.player[x, y], x, y);      //绘制该玩家对应的棋子
                    drawpic(1, x, y);
                    drawpic(0, x, y);                       //在棋子上绘制标记
                    if (qipan.num > 1)                      //重新绘制下的前一颗棋子，去除标记
                    {
                        preplayer = qipan.player[qipan.X[qipan.num - 2], qipan.Y[qipan.num - 2]];
                        drawpic(2, x, y);
                        //drawpic(preplayer, qipan.X[qipan.num - 2], qipan.Y[qipan.num - 2]);
                    }

                    if (qipan.fiveNum(x, y))                //当其中一方获胜时
                    {
                        //int playern = qipan.player[x, y];
                        //MessageBox.Show(str[playern - 1] + "胜出");

                        //win[++win[0]] = playern;            //依次记录获胜的玩家信息
                        //quit[playern - 1] = true;           //获胜用户退出
                        //saveQipan();                        //保存棋盘信息

                        //if (win[0] == win.length - 1)
                        //{
                        if (DialogResult.Yes == MessageBox.Show("再来一盘？", "信息", MessageBoxButtons.YesNo))
                            again();//再来一盘
                        //    else
                        //    {
                        //        button8_click(null, null);
                        //    }
                        //}
                    }
                    pass();                                 //下一位玩家下棋
                }
            }
        }
        private void pass()                                 //让一子 或 交出下子权给下一位玩家
        {
            if (player != null)
            {
                player.next(1);                                 //下一位玩家
                //Tip();                                          //提示落子
            }
        }

        //private void saveQipan()
        //{
        //    if (qipan.num < 3 * 2) return;                                                        //各玩家所下的棋子数目平均小于3则不予保存
        //    string CurDir = System.AppDomain.CurrentDomain.BaseDirectory + @"FiveRecord\";          //设置当前目录
        //    if (!System.IO.Directory.Exists(CurDir)) System.IO.Directory.CreateDirectory(CurDir);   //该路径不存在时，在当前文件目录下创建文件夹FiveRecord

        //    try
        //    {   //不存在该文件时先创建
        //        System.IO.StreamWriter file1 = new System.IO.StreamWriter(CurDir + "五子棋信息记录." + DateTime.Now.Date.ToString("yyyy_MM_dd"), true);

        //        file1.WriteLine("Time: " + DateTime.Now.TimeOfDay.ToString().Substring(0, 8) + "   ");
        //        //file1.WriteLine(setingStr());   //设置信息
        //        //file1.WriteLine(styleStr());    //棋型信息
        //        //file1.WriteLine(controlStr());  //棋盘控制信息
        //        file1.Close();                                          //关闭文件
        //        file1.Dispose();                                        //释放对象
        //    }
        //    catch (Exception)
        //    {
        //        MessageBox.Show("保存棋盘数据失败");
        //        return;
        //    }
        //}



        private void drawpic(int pic, int x, int y)         //在棋盘上坐标(x,y)处绘制下标为pic的image图像
        {
            int X = y * dis + border - dis / 2,             //计算在棋盘上的像素横坐标
                Y = x * dis + border - dis / 2;             //纵坐标
            // g.DrawImage(image[pic], X, Y, (int)(image[pic].Width * s), (int)(image[pic].Height * s));//按s倍大小绘制
            g.DrawImage(image[pic], X, Y, (int)image[pic].Width, (int)image[pic].Height);//按s倍大小绘制
        }

        private void again()
        {
            g.DrawImage(pictureBox1.BackgroundImage, 0, 0, pictureBox1.Width, pictureBox1.Height);  //清空棋盘图形上的棋子,用背景图像重新绘制
            //pictureBox1.Image = pictureBox1.BackgroundImage;
            qipan.clear();                      //清空棋盘上的棋子
            drawQiPan();                        //重绘制棋盘
        }

    }
}