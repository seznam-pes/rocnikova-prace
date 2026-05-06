using Microsoft.VisualBasic.Devices;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
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
            pozadi.Source = new BitmapImage(new Uri("pack://application:,,,/img/menubg.png"));
            title_menu.Visibility = Visibility.Visible;
            game.Visibility = Visibility.Collapsed;
            animace = new Animations(this);
            dialogy = new Dialogues();
            sceny = new Scenes(this, dialogy, animace);
        }

        Dialogues dialogy;
        Animations animace;
        Scenes sceny;
        public List<Button> buttonlist = new List<Button>();

        public int progress = 0;
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

        public void CreateDialogueButton(int x, int y, int id, int sleep)
        {
            var button = new Button();
            button.Opacity = 0;
            buttonlist.Add(button);
            button.Click += (sender, e) => LaunchDialogue(id, sleep);
            main.Children.Add(button);
            button.SetValue(Canvas.LeftProperty, x);
            button.SetValue(Canvas.TopProperty, y);
        }

        public void CreateSceneButton(int x, int y, int id)
        {
            var button = new Button() { Cursor = Cursors.Hand, Content = "TEST",
                Width = 100,
                Height = 50,
                Background = Brushes.Red
            };
            //button.Opacity = 0;
            buttonlist.Add(button);
            button.Click += (sender, e) => pozadi.Source = new BitmapImage(new Uri($"pack://application:,,,/img/{progress}/{id}/backdrop.png"));
            game.Children.Add(button);
            Canvas.SetLeft(button, x);
            Canvas.SetTop(button, y);
            System.Diagnostics.Debug.WriteLine(buttonlist[0]);
        }

        public void LaunchDialogue(int id, int sleep)
        {
            dialoguebg.Visibility = Visibility.Visible;
            dialoguebar.Visibility = Visibility.Visible;
            dialoguebar.Text = dialogy.dialogues[id];
            Thread.Sleep(sleep);
            dialoguebg.Visibility = Visibility.Collapsed;
            dialoguebar.Visibility = Visibility.Collapsed;
        }

        public void NewGame_Click(object sender, RoutedEventArgs e)
        {
            animace.FadeIn(() =>
            {
                title_menu.Visibility = Visibility.Collapsed;
                pozadi.Source = new BitmapImage(new Uri("pack://application:,,,/img/0/0/backdrop.png"));
                CreateSceneButton(1227, 569, 1);
                game.Visibility = Visibility.Visible;
                animace.FadeOut();
            });
            File.WriteAllText("save.json", "0");
        }

        public void Launch_Click(object sender, RoutedEventArgs e)
        {
            ReadSave();
            animace.FadeIn(() =>
            {
                title_menu.Visibility = Visibility.Collapsed;
                pozadi.Source = new BitmapImage(new Uri($"pack://application:,,,/img/{progress}/0/backdrop.png"));
                game.Visibility = Visibility.Visible;
                animace.FadeOut();
            });
        }

        public void Continue_Click(object sender, RoutedEventArgs e)
        {
            pause_menu.Visibility = Visibility.Collapsed;
        }

        public void Quit_Click(object sender, RoutedEventArgs e)
        {
            if (title_menu.Visibility == Visibility.Collapsed)
            {
                WriteSave();
                Thread.Sleep(4000);
                Application.Current.Shutdown();
            }
            else
            {
                Application.Current.Shutdown();
            }
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