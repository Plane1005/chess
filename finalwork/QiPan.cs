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
    }
}
