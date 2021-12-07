using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalwork
{
    class QiPan
    {
        private int Row, Line;                             //棋盘行列数
        private int players;                               //记录玩家的数目
        private int[] count;                               //记录各玩家的棋子数
        public int num;                                    //记录棋盘中的棋子数目
        public int[] X, Y;                                 //记录棋子坐标，按落子次序依次存储
        public int[,] player;                              //记录棋盘的所有棋子，为哪个玩家所下
        private int[,,] style;                            //存储棋型
        public QiPan(int row, int line, int players)
        {
            Row = row;
            Line = line;
            count = new int[players];

            num = 0;
            this.players = players;
            X = new int[row * line];
            Y = new int[row * line];
            player = new int[row, line];

            style = new int[players, row, line];   //1到players存储各个空位置处周边各色棋子的最大棋型棋子数目
        }
        public void put(int x, int y, int player1)
        {
            if (num < X.Length && player[x, y] == 0)        //棋盘未满且当前位置无棋
            {
                count[player1 - 1]++;                         //棋子数加一
                player[x, y] = player1;                     //记录棋子
                X[num] = x;                                 //记录坐标
                Y[num] = y;
                //clearStyle(x, y);                           //清空该坐标处的棋型信息
                //refreshStyle(x, y);                         //更新周边空位置棋型
                num++;                                      //棋子计数
            }
        }
        //判断坐标(x,y)周围是否形成五子
        public bool fiveNum(int x, int y)
        {
            int[] Z = new int[8];               //分别记录八个方向上依次相邻的棋子个数
            int[] A;                            //记录坐标

            for (int i = 1; i <= 8; i++)
            {
                for (int j = 1; j <= 4; j++)    //在每个方向上依次取4个棋子与当前棋子比较
                {
                    A = way(x, y, i, j);        //获取方向i第j颗棋子的坐标
                    if (A[0] != -1)             //正确获取坐标
                        if (player[A[0], A[1]] == player[x, y]) Z[i - 1]++;//统计棋子
                        else break;             //与当前坐标的棋子不同
                    else break;                 //超出棋盘范围
                }
                if (Z[i - 1] == 4) return true; //当某个方向上有四个相邻的棋子时
            }

            //当有四个相邻的棋子时，与当前棋子构成五子
            if (Z[0] + Z[4] >= 4 || Z[1] + Z[5] >= 4 || Z[2] + Z[6] >= 4 || Z[3] + Z[7] >= 4)
                return true;
            else return false;
        }

        //清空棋盘中存储的数据
        public void clear()
        {
            for (int i = 0; i < players; i++) count[i] = 0;
            num = 0;                           //计数值清零
            for (int i = 0; i < X.Length; i++) //坐标清零
            {
                X[i] = 0;
                Y[i] = 0;
            }
            for (int i = 0; i < Row; i++)      //清空 棋子和棋型
                for (int j = 0; j < Line; j++)
                {
                    player[i, j] = 0;
                    clearStyle(i, j);
                }
        }
        //清除棋型信息
        private void clearStyle(int x, int y)
        {
            for (int k = 0; k < players; k++)
                style[k, x, y] = 0;
        }
        private int[] way(int x, int y, int w, int num)
        {   //(X,Y)为8个单位方向向量，分别为朝0度、45度、90度、135度 到315度方向变化
            int[] X = { 0, -1, -1, -1, 0, 1, 1, 1 },
                  Y = { 1, 1, 0, -1, -1, -1, 0, 1 },
                  A = { -1, -1 };
            if (w < 1 || w > 8) return A;   //方向需在[1,8]之间, 5到8分别为1到4的反方向
            A[0] = x + X[w - 1] * num;        //计算行坐标
            A[1] = y + Y[w - 1] * num;        //计算列坐标
            if (0 <= A[0] && A[0] < Row && 0 <= A[1] && A[1] < Line) return A;//在棋盘的坐标范围则返回正确坐标
            else { A[0] = -1; return A; }
        }
    }
}
