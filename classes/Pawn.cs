using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;


namespace VeryGoodChess
{
   public partial class Pawn:Pieces
    {
        public bool direction { get; set; }
        public bool set = true;
        public override List<int[]> GetMoves(Board B)
        {
           
            int d;
            List<int[]> moves = new List<int[]>();
            int[] a = new int[2];
            int[] a1 = new int[2];           
            if (set)
            {
                if (Pos[0] + 2 == 8)
                {
                    direction = true;
                }
                else
                {
                    direction = false;
                }
                set = false;
            }
            a[1] = Pos[1];
            a1[1] = Pos[1];
            if (direction)
            { d = 1; }
            else
            { d = -1; }
            if (Pos[0] >= 1 && Pos[0] <= 6)
            {
                a[0] = Pos[0] - d;
                if (IsFirst&&a[0]>0&&a[0]<7)
                { 
                    a1[0] = Pos[0] - 2 * d;
                    if (B.T[a1[0], a1[1]].I == null && B.T[a[0], a[1]].I == null)
                    {
                        moves.Add(a1);
                       
                    }
                }       
                for (int i = -1; i < 2; i += 2)
                {
                    int[] a2 = new int[2];
                    a2[1] = Pos[1];
                    if ((Pos[1] +  i) >= 0 && (Pos[1] + i) <= 7)
                    {
                        a2[0] = Pos[0] - d;
                        a2[1] = Pos[1] + i;
                        if (B.T[Pos[0] - d, Pos[1] + i].IsB != this.IsB&& B.T[Pos[0] - d, Pos[1] + i].I !=null)
                        {                          
                            moves.Add(a2);
                        }
                        if (B.T[Pos[0] - d, Pos[1] + i].Unpassan&& B.T[Pos[0], Pos[1] + i].IsPawn)
                        {                           
                            moves.Add(a2);                           
                        }
                        
                    }
                }
            }              
                if (B.T[a[0], a[1]].I==null)
                {
                    moves.Add(a);
                }                     
            return moves;
            
        }
        
    }
}
