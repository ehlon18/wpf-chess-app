using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace VeryGoodChess
{
    public class Board
    {
        public Pieces[,] T{ get; set; }
        public int[,] BC(Pieces P)
        { 
            int[,] test = new int[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    test[i, j] = 0;
                    if (T[i, j].I != null)
                    {
                        if (T[i, j].IsB == P.IsB)
                        {
                            test[i, j] = 1;
                        }
                        else
                        {
                            test[i, j] = 2;
                        }
                    }
                }
            }
            return test;
        }
        public void MakeMove(Pieces p, int[] pos)
        {
            T[p.Pos[0], p.Pos[1]] = new Pieces();
            T[pos[0], pos[1]] = new Pieces();
            p.Pos = pos;
            T[pos[0], pos[1]] = p;
        }

        public void UnMakeMove(Pieces p, int[] pos,Pieces pp)
        {
            T[pos[0], pos[1]] = p;
            T[p.Pos[0], p.Pos[1]] = pp;
            p.Pos = pos;
           
            
        }

        public int[] FindKing(bool color)
        {
            int[] a = new int[2];
            foreach (Pieces Pk in T)
            {
                if (Pk.IsKing && (Pk.IsB.Equals(color)))
                {
                    a[0] = Pk.Pos[0];
                    a[1] = Pk.Pos[1];
                    break;
                }

            }

            return a;
        }
        public bool IsInDanger(int[] a, bool color)
        {

            foreach (Pieces p in T)
            {
                if (p.IsB != color && p.I != null)
                {
                    List<int[]> moves = p.GetMoves(this);
                    for (int i = 0; i < moves.Count; i++)
                    {
                        if (moves[i][1] == a[1] && moves[i][0] == a[0])
                        {
                            return true;
                        }

                    }
                }

            }
         
            return false;
        }
        public bool IsInCheck(bool color)
        {
            int[] c= FindKing(color);

            return IsInDanger(c, color);
        }
        public bool IsCastleInDanger(int[] a, bool color)
        {
            foreach (Pieces p in T)
            {
                if (p.IsB != color && p.I != null&&!p.IsKing)
                {
                    List<int[]> moves = p.GetMoves(this);
                    for (int i = 0; i < moves.Count; i++)
                    {
                        if (moves[i][1] == a[1] && moves[i][0] == a[0])
                        {
                            return true;
                        }

                    }
                }

            }

            return false;
        }
        public Pieces SortPiece(string s)
        {
            
            Pieces p=new Pieces();
            p.IsFirst = false;
            p.IsKing = false;
            p.IsBishop = false;
            p.IsRook = false;
            p.IsQueen = false;
            p.Unpassan = false;
            p.IsPawn = false;
            p.IsKnight = false;
          

            if (s.Equals("Wp") || s.Equals("Bp"))
            {
                p = new Pawn();
                p.IsFirst = true;
                p.IsPawn = true;
                p.Value = 100;

            }
            else if (s.Equals("Wr") || s.Equals("Br"))
            {
                p = new Rook();
                p.IsFirst = true;
                p.IsRook = true;
                p.Value = 500;
            }
            else if (s.Equals("Wb") || s.Equals("Bb"))
            {
                p = new Bishop();
                p.IsBishop = true;
                p.Value = 300;
            }
            else if (s.Equals("Bkn") || s.Equals("Wkn"))
            {
                p = new Knight();
                p.Value = 300;
                p.IsKnight = true;

            }
            else if (s.Equals("Wq") || s.Equals("Bq"))
            {
                p = new Queen();
                p.Value = 900;
                p.IsQueen = true;
               
            }
            else
            {
                p = new King();
                p.IsFirst = true;
                p.IsKing = true;              
            }
           
            
            if (s.Contains('W'))
            {
                p.IsB = false;
            }
            else 
            {
                p.IsB = true;
            }
            return p;
        }
    }
}
