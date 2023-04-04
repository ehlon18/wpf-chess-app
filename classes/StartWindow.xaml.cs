using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace VeryGoodChess
{
    /// <summary>
    /// Interaction logic for StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        bool islight = true;
        
        public StartWindow()
        {
            InitializeComponent();
          chessi.Source = new BitmapImage(new Uri(@"/chess/start.svg.png", UriKind.Relative));


        }

        private void startGame(object sender, RoutedEventArgs e)
        {
            int a = 0;
            if(ColorComboBox.SelectedIndex== 0) 
            { 
            }
            else if (ColorComboBox.SelectedIndex== 1) 
            {
                a = 1;
            }
            else
            {
                a = 2;
            }
            Window Window2 = new MainWindow(a,islight);         
            Window2.Show();
            this.Close();
           

        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            play.Foreground = Brushes.Black;
            s.Background = Brushes.WhiteSmoke;
            
        }

        private void play_MouseLeave(object sender, MouseEventArgs e)
        {
            play.Foreground = Brushes.WhiteSmoke;
            s.Background = Brushes.Black;
        }
        private void Button_MouseEnter1(object sender, MouseEventArgs e)
        {
            closet.Foreground = Brushes.Black;
            close.Background = Brushes.WhiteSmoke;

        }

        private void play_MouseLeave1(object sender, MouseEventArgs e)
        {
            closet.Foreground = Brushes.WhiteSmoke;
            close.Background = Brushes.Black;
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void mode_Click(object sender, RoutedEventArgs e)
        {
            if (islight)
            {
                islight = false;
                mode.Background= Brushes.Black;
                gamemodetext.Foreground= Brushes.White;
                gamemodetext.Text = "Game Mode: Dark";
            }
            else
            {
                islight = true;
                mode.Background = Brushes.White;
                gamemodetext.Foreground = Brushes.Black;
                gamemodetext.Text = "Game Mode: Light";
            }


        }
    }

}
