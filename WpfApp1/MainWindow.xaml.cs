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
using System.Windows.Media.Animation;
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

        int progress = 0;
        string str_progress = "0";

        public void WriteSave()
        {
            str_progress = progress.ToString();
            File.WriteAllText("save.json", str_progress);
        }

        public void ReadSave()
        {
            try
            {
                string tempstr = File.ReadAllText("save.json");
                progress = int.Parse(tempstr);
            }
            catch (FileNotFoundException)
            {
                File.WriteAllText("save.json", "");
            }
        }

        public void FadeIn(Action? onComplete = null)
        {
            rect.Visibility = Visibility.Visible;
            rect.Opacity = 0;

            var anim = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(3),
                EasingFunction = new QuadraticEase
                {
                    EasingMode = EasingMode.EaseOut
                }
            };

            anim.Completed += (s, e) =>
            {
                onComplete?.Invoke();
            };

            rect.BeginAnimation(UIElement.OpacityProperty, anim);
        }

        public void FadeOut(Action? onComplete = null)
        {
            var anim = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(3),
                EasingFunction = new QuadraticEase
                {
                    EasingMode = EasingMode.EaseOut
                }
            };

            anim.Completed += (s, e) =>
            {
                rect.Visibility = Visibility.Collapsed;
            };

            rect.BeginAnimation(UIElement.OpacityProperty, anim);
        }

        public void SwitchScene()
        {
            FadeIn();
            progress++;
            WriteSave();
            Thread.Sleep(3000);
            FadeOut();
        }

        public void NewGame_Click(object sender, RoutedEventArgs e)
        {
            FadeIn(() =>
            {
                title_menu.Visibility = Visibility.Collapsed;
                pozadi.Source = new BitmapImage(new Uri("pack://application:,,,/img/0/backdrop.png"));
                game.Visibility = Visibility.Visible;
                FadeOut();
            });
            File.WriteAllText("save.json", "0");
        }

        public void Launch_Click(object sender, RoutedEventArgs e)
        {
            ReadSave();
            FadeIn(() =>
            {
                title_menu.Visibility = Visibility.Collapsed;
                pozadi.Source = new BitmapImage(new Uri($"pack://application:,,,/img/{progress}/backdrop.png"));
                game.Visibility = Visibility.Visible;
                FadeOut();
            });
        }

        public void Continue_Click(object sender, RoutedEventArgs e)
        {
            pause_menu.Visibility = Visibility.Collapsed;
        }

        public void Quit_Click(object sender, RoutedEventArgs e)
        {
            WriteSave();
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