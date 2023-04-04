using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeryGoodChess
{
    public class Knight: Pieces
    {
        public override List<int[]> GetMoves( Board B)
        {
           
            List<int[]> moves = new List<int[]>();            
            for (int i = 0; i < 4; i++)
            {
               
                for (int j = 0; j < 2; j++)
                {
                    int[] a = new int[2];
                    if(i==1)
                    {
                        if (j == 0)
                        {
                            a[0] = Pos[0] - 2;
                            a[1] = Pos[1] - 1;
                        }
                        else
                        {
                            a[0] = Pos[0] - 2;
                            a[1] = Pos[1] + 1;
                        }

                    }
                    else if(i==3)
                    {
                        if (j == 0)
                        {
                            a[0] = Pos[0] + 2;
                            a[1] = Pos[1] - 1;
                        }
                        else
                        {
                            a[0] = Pos[0] + 2;
                            a[1] = Pos[1] + 1;
                        }
                    }
                    else if(i==2)
                    {
                        if (j == 0)
                        {
                            a[0] = Pos[0] -1;
                            a[1] = Pos[1] + 2;
                        }
                        else
                        {
                            a[0] = Pos[0] +1;
                            a[1] = Pos[1] -2;
                        }
                    }
                    else
                    {
                        if (j == 0)
                        {
                            a[0] = Pos[0] + 1;
                            a[1] = Pos[1] + 2;
                        }
                        else
                        {
                            a[0] = Pos[0] - 1;
                            a[1] = Pos[1] - 2;
                        }
                    }
                    if ((a[0] >= 0 && a[0] <= 7) && (a[1] >= 0 && a[1] <= 7))
                    {
                        if(B.T[a[0],a[1]].IsB!=this.IsB|| B.T[a[0], a[1]].I==null)
                           moves.Add(a);
                    }
                }
            }
           
           
            return moves;

        }
    }
}
