using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows;

namespace VeryGoodChess
{
     public class Pieces:Window
    {
        
        public Image I { get; set; }
        public bool IsB;
        public float Value { get; set; }
        public float Sum { get; set; }
        public int[] BestMove { get; set; }
        public float Bestvalue { get; set; }
        public int[] Pos { get; set; }
        public bool IsP { get; set; }
        public bool IsFirst { get; set; }
        public bool Unpassan { get; set; } 
        public bool IsKing { get; set; }
        public bool IsPawn { get; set; }
        public bool IsBishop { get; set; }
        public bool IsKnight { get; set; }
        public bool IsQueen { get; set; }
        public bool IsRook { get; set; }
        public bool Goodstart { get; set; }
        public string Name { get; set; }
        public string PNG { get; set; }
        public int[] GetPos(string s)
        {
            int[] pos = new int[2];
            int col;
            var row = s.Substring(0, (int)(s.Length / 2));
            pos[0] = int.Parse(row.ToString());
            var last = s.Substring(1, (int)(s.Length / 2));
            if (last.Equals("a"))
            {
                col = 0;
            }
            else if (last.Equals("b"))
            {
                col = 1;
            }
            else if (last.Equals("c"))
            {
                col = 2;
            }
            else if (last.Equals("d"))
            {
                col = 3;
            }
            else if (last.Equals("e"))
            {
                col = 4;
            }
            else if (last.Equals("f"))
            {
                col = 5;
            }
            else if (last.Equals("g"))
            {
                col = 6;
            }
            else
            {
                col = 7;
            }

            pos[1] = col;
            return pos;
            
        }
        public string GetPng(string name)
        {
            if(name.Equals("Wp"))
            {
                return @"pack://application:,,,/chess/Wp.svg.png";
               
            }
            if (name.Equals("Bp"))
            {
                IsB = true;
                return @"pack://application:,,,/chess/Bp.svg.png";
            }
            if (name.Equals("Wr"))
            {
                return @"pack://application:,,,/chess/Wr.svg.png";
            }
            if (name.Equals("Br"))
            {
                IsB = true;
                return @"pack://application:,,,/chess/Br.svg.png";

            }
            if (name.Equals("Wb"))
            {
                return @"pack://application:,,,/chess/Wb.svg.png";
            }
            if (name.Equals("Bb"))
            {
                IsB = true;
                return @"pack://application:,,,/chess/Bb.svg.png";
            }
            if (name.Equals("Bkn"))
            {
                IsB = true;
                return @"pack://application:,,,/chess/Bkn.svg.png";
            }
            if (name.Equals("Wkn"))
            {
                return @"pack://application:,,,/chess/Wkn.svg.png";
            }
            if (name.Equals("Wq"))
            {
                return @"pack://application:,,,/chess/Wq.svg.png";
            }
            if (name.Equals("Bq"))
            {
                IsB = true;
                return @"pack://application:,,,/chess/Bq.svg.png";
            }
            if (name.Equals("Wk"))
            {
                return @"pack://application:,,,/chess/Wk.svg.png";
            }
            else
            {
                IsB = true;
                return @"pack://application:,,,/chess/Bk.svg.png";
            }
        }
        public virtual List<int[]> GetMoves(Board B)
        {
            
            List<int[]> moves = new List<int[]>();
            int n0, n1;
            bool g1, g2;
            for (int j = 0; j < 4; j++)
            {
                g1 = true;
                g2 = true;
                if (j == 0)
                {
                    n0 = 1;
                    n1 = -1;

                }
                else if (j == 1)
                {
                    n0 = -1;
                    n1 = 1;

                }
                else if (j == 2)
                {
                    n0 = -1;
                    n1 = -1;

                }
                else
                {
                    n0 = 1;
                    n1 = 1;

                }
                for (int i = 1; i < 8; i++)
                {
                    int[] a = new int[2];
                    int[] a1 = new int[2];
                    a1[0] = Pos[0];
                    a1[1] = Pos[1];
                    a[0] = Pos[0] + (1 * n0) * i;
                    a[1] = Pos[1] + (1 * n1) * i;

                    if (n0 > 0)
                    {
                        if (n1 > 0)
                        {
                            a1[0] = Pos[0] + (1 * n0) * i;
                        }
                        else
                        {
                            a1[1] = Pos[1] + (1 * n1) * i;

                        }
                    }
                    else
                    {
                        if (n1 > 0)
                        {
                            a1[1] = Pos[1] + (1 * n1) * i;
                        }
                        else
                        {
                            a1[0] = Pos[0] + (1 * n0) * i;

                        }
                    }
                    if (IsBishop||IsQueen)
                    {
                        if ((a[0] >= 0 && a[0] <= 7) && (a[1] >= 0 && a[1] <= 7) && g1)
                        {
                            if (B.T[a[0], a[1]].I == null)
                            {
                                moves.Add(a);
                            }
                            else if (B.T[a[0], a[1]].IsB != this.IsB)
                            {
                                moves.Add(a);
                                g1 = false;
                            }
                            else
                                g1 = false;
                        }
                    }
                    if (IsRook || IsQueen)
                    {
                        if ((a1[0] >= 0 && a1[0] <= 7) && (a1[1] >= 0 && a1[1] <= 7) && g2)
                        {
                            if (B.T[a1[0], a1[1]].I==null)
                            {
                                moves.Add(a1);
                            }
                            else if (B.T[a1[0], a1[1]].IsB != this.IsB)
                            {
                                moves.Add(a1);
                                g2 = false;
                            }
                            else
                                g2 = false;
                        }
                    }
                    if (!(a[0] >= 0 && a[0] <= 7) && (a[1] >= 0 && a[1] <= 7) && !(a1[0] >= 0 && a1[0] <= 7) && (a1[1] >= 0 && a1[1] <= 7))
                    {
                        break;
                    }
                }
            }
            return moves;
        }
        }
    
}
