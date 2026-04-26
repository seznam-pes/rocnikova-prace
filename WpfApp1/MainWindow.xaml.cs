using Microsoft.VisualBasic.Devices;
using NAudio.Wave;
using System.Configuration.Internal;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Media;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void Launch_Click(object sender, RoutedEventArgs e)
        {
            pozadi.Source = new BitmapImage(new Uri("pack://application:,,,/img/scene1.png"));
            title_menu.Visibility = Visibility.Collapsed;
            game.Visibility = Visibility.Visible;
        }

        public void Continue_Click(object sender, RoutedEventArgs e)
        {
            pause_menu.Visibility = Visibility.Collapsed;
        }

        public void Quit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        public void EscMenu_KeyPress(object sender, KeyEventArgs e)
        {
            if (title_menu.Visibility == Visibility.Collapsed)
            {
                if (e.Key == Key.Escape)
                {
                    if (pause_menu.Visibility == Visibility.Visible)
                    {
                        pause_menu.Visibility = Visibility.Collapsed;
                        game.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        pause_menu.Visibility = Visibility.Visible;
                        game.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }
    }
}