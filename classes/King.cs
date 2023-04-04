using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace VeryGoodChess
{
    public class King : Pieces
    {
       public Board C { get; set; } 
        public override List<int[]> GetMoves(Board B)
        {
           
            List<int[]> moves = new List<int[]>();
            int n0, n1,n2;
            for (int j = 0; j < 2; 
                j++)
            {
                if (j == 0)
                {
                    n0 = 1;
                    n1 = -1;
                }              
                else
                {
                    n0 = -1;
                    n1 = 1;
                }
               
                for (int i = 0; i<2; i++)
                {
                    int[] a = new int[2];
                    int[] c = new int[2];
                    if (i==0)
                    {
                        n2 = 1;
                        a[0] = Pos[0] + (1 * n0);
                        a[1] = Pos[1];
                        c[0] = Pos[0] + (1 * n0) * n2;
                        c[1] = Pos[1] + (1 * n1) ;
                    }
                    else
                    {
                        n2 = -1;                       
                        a[1] = Pos[1] + (1 * n1);
                        a[0]=Pos[0];
                        c[0] = Pos[0] + (1 * n0) ;
                        c[1] = Pos[1] + (1 * n1) * n2;
                    }
                   
                    if((a[0] >= 0 && a[0] <= 7) && (a[1] >= 0 && a[1] <= 7))
                    {
                        if((B.T[a[0], a[1]].IsB != IsB || B.T[a[0], a[1]].I == null))
                        moves.Add(a);
                   
                    }
                    if ((c[0] >= 0 && c[0] <= 7) && (c[1] >= 0 && c[1] <= 7))
                    {
                        if((B.T[c[0], c[1]].IsB != IsB || B.T[c[0], c[1]].I == null))
                        moves.Add(c);
                    }

                }
            }
            if (IsFirst)
            {
                foreach (int[] move in CanCastle(B))
                {
                    moves.Add(move);
                }
            }
            return moves;
        }
        public List<int[]> CanCastle(Board B)
        {
            List<int[]> Castles=new List<int[]>();
            int[] Castle = new int[2];
            int[] Ic = new int[2];
            Ic[0]=Pos[0];
            Ic[1]=Pos[1];
            if (!B.IsCastleInDanger(Pos, IsB) )
            {
                if (Pos[1] == 3)
                {
                    if (B.T[Pos[0], 0].IsFirst)
                    {
                        for (int i = 1; i < 3; i++)
                        {
                            Ic[1] = Ic[1] - i;
                           
                                if ((B.IsCastleInDanger(Ic, IsB) || B.T[Ic[0], Ic[1]].I != null))
                                {
                                    break;
                                }
                                else if (i == 2)
                                {
                                    Castle[0] = Pos[0];
                                    Castle[1] = Pos[1] - 2;
                                    Castles.Add(Castle);

                                }
                          
                            Ic[1] = Pos[1];
                        }
                    }
                    Ic[1] = Pos[1];
                    Castle = new int[2];
                    if (B.T[Pos[0], 7].IsFirst)
                    {
                        for (int i = 1; i < 4; i++)
                        {
                            Ic[1] = Ic[1] + i;
                            if (B.IsCastleInDanger(Ic, IsB) || B.T[Ic[0], Ic[1]].I != null)
                            {
                                break;
                            }
                            else if (i == 3)
                            {
                                Castle[0] = Pos[0];
                                Castle[1] = Pos[1] + 2;
                                Castles.Add(Castle);

                            }
                            Ic[1] = Pos[1];

                        }
                    }
                }
                else
                {

                    if (B.T[Pos[0], 0].IsFirst)
                    {
                        for (int i = 1; i < 4; i++)
                        {
                            Ic[1] = Ic[1] - i;
                          
                                if ((B.IsCastleInDanger(Ic, IsB) || B.T[Ic[0], Ic[1]].I != null))
                                {
                                    break;
                                }
                                else if (i == 3)
                                {
                                    Castle[0] = Pos[0];
                                    Castle[1] = Pos[1] - 2;
                                    Castles.Add(Castle);

                                }
                          
                            Ic[1] = Pos[1];
                        }
                    }
                    Ic[1] = Pos[1];
                    Castle = new int[2];
                    if (B.T[Pos[0], 7].IsFirst)
                    {
                        for (int i = 1; i < 3; i++)
                        {
                            Ic[1] = Ic[1] + i;
                            if (B.IsCastleInDanger(Ic, IsB) || B.T[Ic[0], Ic[1]].I != null)
                            {
                                break;
                            }
                            else if (i == 2)
                            {
                                Castle[0] = Pos[0];
                                Castle[1] = Pos[1] + 2;
                                Castles.Add(Castle);

                            }
                            Ic[1] = Pos[1];

                        }
                    }
                }
                
            }
            return Castles;
          

        }


    }
         
}
