using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Media;

namespace VeryGoodChess
{
    public partial class MainWindow : Window
    {
        
        Board B = new Board();
        Board K = new Board();
        Board A = new Board();
        Border[,] z = new Border[8, 8];
        bool IsHoLd = true, Turn = true,White=true,TPIsNotNull=false,Ep=false;
        Pieces ThisP, unp;
        int canu=0;
        int Data = 0;       
        int[] upos = new int[2];
       
        public MainWindow(int data, bool islight)
        {
            
           
            Data = data;
            B.T = new Pieces[8,8];
            K.T = new Pieces[8,8];
            A.T = new Pieces[8,8];
            InitializeComponent();

            this.ChessBoard.MouseLeftButtonDown += new MouseButtonEventHandler(GridCtrl_MouseDown);
            this.ChessBoard.MouseLeftButtonUp += new MouseButtonEventHandler(GridCtrl_MouseUp);
            if (islight)
            {
                this.Background = Brushes.White;
                ChekText.Foreground = Brushes.Black;
                Wintext.Foreground = Brushes.Black;
            }
            else
            {
                this.Background = Brushes.Black;
                ChekText.Foreground = Brushes.White;
                Wintext.Foreground = Brushes.White;
            }
            SetUpBoard();
          
            for (int i = 1; i < 2; i++)
            {
                EqualBoardsA(A, false);               
            }           
        }
        
        public void SetUpBoard()
        {
            Turn = true;
            ChekText.Text = "";
            Border [,]b =new Border[8,8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    A.T[i, j] = new Pieces();
                    K.T[i, j] = new Pieces();
                    B.T[i, j] = new Pieces();
                    B.T[i, j].Pos = new int[2];
                    B.T[i, j].Pos[0] = i;
                    B.T[i, j].Pos[1] = j;
                    B.T[i, j].IsP = false;
                    b[i, j] = new Border();
                    ChessBoard.Children.Add(b[i, j]);
                    Grid.SetColumn(b[i,j], i);
                    Grid.SetRow(b[i,j],j);
                    if ((i + j) % 2 != 0)
                    {if(Data==0)
                        b[i, j].Background = Brushes.SaddleBrown;
                    else if(Data==1)
                            b[i, j].Background = Brushes.Green;
                    else
                            b[i, j].Background = Brushes.DarkSlateGray;
                    }
                    else
                        b[i, j].Background = Brushes.White;
                   
                    b[i, j].BorderThickness = new Thickness(1, 1, 1, 1);
                   
                }              
            }
            z = b;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    B.T[i, j].Pos[0] = j;
                    B.T[i, j].Pos[1] = i;
                }
            }
            Random rnd=new Random();
            string s;
            if (rnd.Next(0, 2) % 2 == 0)
            {
                s = "Wr.8a/Wkn.8b/Wb.8c/Wk.8e/Wq.8d/Wb.8f/Wkn.8g/Wr.8h/Wp.7a/Wp.7b/Wp.7c/Wp.7d/Wp.7e/Wp.7f/Wp.7g/Wp.7h/Br.1a/Bkn.1b/Bb.1c/Bk.1e/Bq.1d/Bb.1f/Bkn.1g/Br.1h/Bp.2a/Bp.2b/Bp.2c/Bp.2d/Bp.2e/Bp.2f/Bp.2g/Bp.2h";
                White = true;
            }
            else
            {
                 s = "Br.8a/Bkn.8b/Bb.8c/Bk.8d/Bq.8e/Bb.8f/Bkn.8g/Br.8h/Bp.7a/Bp.7b/Bp.7c/Bp.7d/Bp.7e/Bp.7f/Bp.7g/Bp.7h/Wr.1a/Wkn.1b/Wb.1c/Wk.1d/Wq.1e/Wb.1f/Wkn.1g/Wr.1h/Wp.2a/Wp.2b/Wp.2c/Wp.2d/Wp.2e/Wp.2f/Wp.2g/Wp.2h";
                White = false;
            }
            string[] subs = s.Split('/', '.');
            for (int i = 0; i < subs.Length; i++)
            {
                if (i % 2 == 0)
                {
                    int[] a=new int[2];
                    int x, y;
                    Pieces p = new Pieces();
                    p = B.SortPiece(subs[i]);
                    p.Pos = a;
                    p.I = new Image();
                    p.Name = subs[i];                             
                    x = p.GetPos(subs[i + 1])[0]-1;
                    y = p.GetPos(subs[i + 1])[1];
                    p.PNG = p.GetPng(p.Name);
                    p.Pos[0] = x;
                    p.Pos[1] = y;
                    p.I.Source = new BitmapImage(new Uri(p.PNG));
                    p.IsP = true;
                    Grid.SetRow(p.I, x);
                    Grid.SetColumn(p.I, y);
                    ChessBoard.Children.Add(p.I);
                    B.T[x, y] = p;                   
                }
            }
            
        }

        public void SetUpTile(Pieces p,int x,int y)
        {
            int a, b;

            if (canu > 0)
            {
                unp.Unpassan = false;
                canu = 0;
            }
            if (p.IsPawn)
            { 
                if(p.IsFirst)
                {
                    if (x == 3)
                    {
                        canu++;
                        B.T[2, p.Pos[1]].Unpassan = true;
                        unp =B.T[2, p.Pos[1]];
                    }
                    else if (x == 4)
                    {
                        canu++;
                        B.T[5, p.Pos[1]].Unpassan = true;
                        unp = B.T[5, p.Pos[1]];
                    }
                }           
                if (!B.T[x, y].IsP&&y!=p.Pos[1])
                {   int x2;
                    x2 = x - p.Pos[0];
                    RemovePiece(B.T[x - x2, y]);                   
                }
                if(x==0||x==7)
                {
                    Ep = true;
                    EndPawn.Visibility = Visibility.Visible;
                    ChessBoard.IsEnabled = false;
                    if (p.IsB)
                    {
                        ShowKnigt.Source = new BitmapImage(new Uri( p.GetPng("Bkn")));
                        ShowQueen.Source = new BitmapImage(new Uri(p.GetPng("Bq")));
                        ShowBishop.Source = new BitmapImage(new Uri(p.GetPng("Bb")));
                        ShowRook.Source = new BitmapImage(new Uri(p.GetPng("Br")));

                    }
                    else
                    {
                        ShowKnigt.Source = new BitmapImage(new Uri(p.GetPng("Wkn")));
                        ShowQueen.Source = new BitmapImage(new Uri(p.GetPng("Wq")));
                        ShowBishop.Source = new BitmapImage(new Uri(p.GetPng("Wb")));
                        ShowRook.Source = new BitmapImage(new Uri(p.GetPng("Wr")));
                    }
                 
                    upos[0] = x;
                    upos[1] = y;
                }
            }            
                if (B.T[x, y].IsP)
                {
                    int[] pos = new int[2];
                    pos[0] = x;
                    pos[1] = y;
                    RemovePiece(B.T[x, y]);
                }           
                a = p.Pos[0];
                b = p.Pos[1];
                p.Pos[0] = x;
                p.Pos[1] = y;
                p.I.Source = new BitmapImage(new Uri(p.PNG));
                p.IsP = true;
                Grid.SetRow(p.I, x);
                Grid.SetColumn(p.I, y);
                ChessBoard.Children.Add(p.I);
                B.T[x, y] = p;
                B.T[a, b] = new Pieces();
           
        }
        public void RemovePiece(Pieces p)
        {
            ChessBoard.Children.Remove(p.I);
          
            B.T[p.Pos[0], p.Pos[1]] = new Pieces();
            p.IsP=false;                              
        }

        public void MovePiece(int[] a,Pieces TP)
        {
            RemovePiece(TP);
            SetUpTile(TP, a[0], a[1]);
            int blackp=0,whitep=0;
            bool knightorbishop =false;
            foreach (Pieces P in B.T)
            {
                if(P.IsB&&P.I!=null) blackp++;
                else if(!P.IsB && P.I != null) whitep++;

                if(P.I!=null&&(P.IsBishop||P.IsKnight))
                {
                  knightorbishop= true;
                }
            }
                if (TP.IsFirst)
                {
                    TP.IsFirst = false;
                }
                Turn = (TP.IsB);
                bool c = !TP.IsB;

            if (B.IsInDanger(B.FindKing(c), c))
            {
                ChekText.Text = "chek";
                if (IsMate(c, B))
                {
                    ChekText.Text = "checkmate";
                    if (TP.IsB)
                        Wintext.Text = "black won";
                    else
                        Wintext.Text = "white won";
                }
            }
            else if (whitep == 1 && blackp == 1)
            {
                ChekText.Text = "lack of matrial";
                Wintext.Text = "draw";
                ChessBoard.IsEnabled = false;
            }
            else if((whitep == 1 && blackp == 2)|| (whitep == 2 && blackp == 1))
            {
                if(knightorbishop)
                {
                    ChekText.Text = "lack of matrial";
                    Wintext.Text = "draw";
                    ChessBoard.IsEnabled = false;
                }
            }
            else if(IsStalemate(B,TP.IsB))
            {
                ChekText.Text = "stalmate";
                Wintext.Text = "draw";
                ChessBoard.IsEnabled = false;
            }
                else
                { ChekText.Text = "";
               
                 }
        }

        private void GridCtrl_MouseDown(object sender, MouseButtonEventArgs e)
        {
           
            if (Turn!= B.T[GetGridPos()[0], GetGridPos()[1]].IsB)
            {
                if (ThisP == null && B.T[GetGridPos()[0], GetGridPos()[1]].I != null)
                {
                    
                    ThisP = B.T[GetGridPos()[0], GetGridPos()[1]];
                    ShowMoves(ActualMoves(ThisP, B));
                    TPIsNotNull = true;                  
                }
               else if(B.T[GetGridPos()[0], GetGridPos()[1]]!= ThisP && B.T[GetGridPos()[0], GetGridPos()[1]].I != null)
                {   PaintBoard();
                    ThisP = B.T[GetGridPos()[0], GetGridPos()[1]];
                    ShowMoves(ActualMoves(ThisP, B));                    
                    TPIsNotNull = true;
                    if (B.T[GetGridPos()[0], GetGridPos()[1]].IsB != ThisP.IsB)
                    {                       
                        Turn = !Turn;
                        
                    }
                }
                
            }
        }

        private void GridCtrl_MouseUp(object sender, MouseButtonEventArgs e)
        {
               
                if (ThisP != null&&IsHoLd)
                {                    
                    bool c = !ThisP.IsB;
                    if (Turn != ThisP.IsB)
                    {
                        List<int[]> a;
                    if (ThisP != null)
                    {
                        a = ActualMoves(ThisP, B);                       
                        foreach (int[] move in a)
                            {
                                if (move[0] == GetGridPos()[0] && move[1] == GetGridPos()[1])
                                {
                                    int[] cp = new int[2];
                                    cp[0] = ThisP.Pos[0];
                                    cp[1] = ThisP.Pos[1];                                  
                                    MovePiece(GetGridPos(), ThisP);
                                    if (ThisP.IsKing)
                                    {
                                        int distance = move[1] - cp[1];
                                    if (!White)
                                    {
                                        if (Math.Abs(distance) > 1)
                                        {
                                            if (distance < 0)
                                            {
                                                cp[1] = cp[1] - 1;
                                                MovePiece(cp, B.T[cp[0], cp[1] - 2]);
                                            }
                                            else
                                            {
                                                cp[1] = cp[1] + 1;
                                                MovePiece(cp, B.T[cp[0], cp[1] + 3]);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (Math.Abs(distance) > 1)
                                        {
                                            if (distance < 0)
                                            {
                                                cp[1] = cp[1] - 1;
                                                MovePiece(cp, B.T[cp[0], cp[1] - 3]);
                                            }
                                            else
                                            {
                                                cp[1] = cp[1] + 1;
                                                MovePiece(cp, B.T[cp[0], cp[1] + 2]);
                                            }
                                        }
                                    }
                                    }

                                    PaintBoard();
                                    IsHoLd = false;
                                    if(!Ep)
                                    ThisP = null;
                                    break;
                                }
                            }                      
                    }
                    }
                }
               
            
            if (TPIsNotNull)
            {

                IsHoLd = true;
            }
            TPIsNotNull = false;
        }
        public bool IsMate(bool color,Board S)
        {
            foreach (Pieces p in S.T)
            {
                if ((p.IsB == color) && p.I != null)
                {
                    List<int[]> moves = ActualMoves(p,S);
                    if(moves.Count!=0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public int[] GetGridPos()
        {
            int[] ClickPos=new int[2];
            var point = Mouse.GetPosition(ChessBoard);
            int row = 0;
            int col = 0;
            double accumulatedHeight = 0.0;
            double accumulatedWidth = 0.0;           
            foreach (var rowDefinition in ChessBoard.RowDefinitions)
            {
                accumulatedHeight += rowDefinition.ActualHeight;
                if (accumulatedHeight >= point.Y)
                    break;
                row++;
            }        
            foreach (var columnDefinition in ChessBoard.ColumnDefinitions)
            {
                accumulatedWidth += columnDefinition.ActualWidth;
                if (accumulatedWidth >= point.X)
                    break;
                col++;
            }
            ClickPos[0] = row;
            ClickPos[1] = col;            
            return ClickPos;
        }     
        public void PaintBoard()
        {
            Border[,] b = z;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((i + j) % 2 != 0)
                    {
                        if (Data == 0)
                            b[i, j].Background = Brushes.SaddleBrown;
                        else if (Data == 1)
                            b[i, j].Background = Brushes.Green;
                        else
                            b[i, j].Background = Brushes.DarkSlateGray;
                    } 
                    else
                        b[i, j].Background = Brushes.White;
                }
            }
        }
        public void ShowMoves(List<int[]> AllMoves)
        {
            Border[,] b = z;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {               
                    foreach (int[] move in AllMoves)
                    {
                        if (move[0] == i && move[1] == j)
                        {                          
                            b[j, i].Background = Brushes.Blue;
                        }
                    }
                }
            }
        }
        public List<int[]> ActualMoves(Pieces p,Board S)
        {
            List<int[]> Amoves = new List<int[]>();          
            EqualBoards(S);
            int[] castle = new int[2];
            List<int[]> moves =p.GetMoves(K);
            for (int i = 0; i < moves.Count; i++)
            {   Pieces pp=new Pieces();
                
                    int[] previospos = new int[2];
                    previospos = p.Pos;
                    Pieces PreviosP = K.T[moves[i][0], moves[i][1]];
                    K.MakeMove(p, moves[i]);
                    int[] c = K.FindKing(p.IsB);
                    if (!K.IsInDanger(c, p.IsB))
                    {
                        Amoves.Add(moves[i]);
                    }
                    K.UnMakeMove(p, previospos, PreviosP);               
            }              
            return Amoves;            
       }
        public void EqualBoards(Board S)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {

                    K.T[i, j] = new Pieces();
                    K.T[i, j] = S.T[i, j];
                    K.T[i, j].Pos = S.T[i, j].Pos;

                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window Window2 = new StartWindow();
           
            Window2.Show();
            this.Close();
        }

        public void ChangePawn(Pieces End)
        {
           Pieces l = new Pieces();
           l = End;
           l.Pos = upos;
           EndPawn.Visibility = Visibility.Hidden;
           l.IsB = Turn;              
           ChessBoard.Children.Remove(B.T[upos[0], upos[1]].I);
           Grid.SetRow(l.I, upos[0]);
           Grid.SetColumn(l.I, upos[1]);
           ChessBoard.Children.Add(l.I);
           B.T[upos[0], upos[1]] = l;
           B.T[upos[0], upos[1]].PNG = l.PNG;
           B.T[upos[0], upos[1]].IsP = true;
           ChessBoard.IsEnabled = true;          
            Ep = false;
            if (IsMate(!l.IsB,B))
            { Restart(); }
        }

        private void MakeKnigt_Click(object sender, RoutedEventArgs e)
        {
            Pieces End = new Knight();
            End.I = new Image();
            End.Pos = new int[2];
            if(Turn)
                End.Name="Bkn";
            else
                End.Name = "Wkn";
            End.I.Source = new BitmapImage(new Uri(End.GetPng(End.Name)));
            End.PNG = End.GetPng(End.Name);
            ChangePawn(End);
        }
        private void MakeQueen_Click(object sender, RoutedEventArgs e)
        {
            Pieces End = new Queen();
            End.I = new Image();
            End.Pos = new int[2];
            End.IsQueen = true;
            if (Turn)
                End.Name = "Bq";
            else
                End.Name = "Wq";
            End.I.Source = new BitmapImage(new Uri(End.GetPng(End.Name)));
            End.PNG = End.GetPng(End.Name);
            ChangePawn(End);
        }
        private void MakeBishop_Click(object sender, RoutedEventArgs e)
        {
            Pieces End = new Bishop();
            End.I = new Image();
            End.Pos = new int[2];
            End.IsBishop = true;
            if (Turn)
                End.Name = "Bq";
            else
                End.Name = "Wq";
            End.I.Source = new BitmapImage(new Uri(End.GetPng(End.Name)));
            End.PNG = End.GetPng(End.Name);
            ChangePawn(End);
        }
        private void MakeRook_Click(object sender, RoutedEventArgs e)
        {
            Pieces End = new Rook();
            End.I = new Image();
            End.Pos = new int[2];
            End.IsRook = true;
            if (Turn)
                End.Name = "Bq";
            else
                End.Name = "Wq";
            End.I.Source = new BitmapImage(new Uri(End.GetPng(End.Name)));
            End.PNG = End.GetPng(End.Name);
            ChangePawn(End);
        }
       
        private void RestartGame(object sender, RoutedEventArgs e)
        {
            Restart();
        }
        
        public void Restart()
        {
            InitializeComponent();
            ChessBoard.IsEnabled = true;
            Wintext.Text = "";
            SetUpBoard();
        }
        public bool IsStalemate(Board S,bool color)
        {
            List<int[]> a;
            foreach (Pieces piece in S.T)
            {
                if (piece.I!= null&&piece.IsB!=color)
                { a = ActualMoves(piece, S);
                    if(a.Count!=0)
                    {
                        return false;
                    }
                }

            }
            return true;
        }
        public void EqualBoardsA(Board S,bool sumr)
        {

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    S.T[i, j] = new Pieces();
                    S.T[i, j] = B.T[i, j];
                    S.T[i, j].Pos = B.T[i, j].Pos;
                }
            }
        }     
    }
}
