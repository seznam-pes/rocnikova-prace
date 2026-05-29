using Microsoft.VisualBasic.Devices;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System.Configuration.Internal;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Media;
using System.Security.Cryptography.X509Certificates;
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
using static WpfApp1.Scenes;

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

        public class localAxis()
        {
            public int localX;
            public int localY;
        }

        Dialogues dialogy;
        Animations animace;
        Scenes sceny;
        public List<Button> buttonlist = new List<Button>();
        private List<Image> inventorySlots;
        private List<int> inventoryItems = new();
        public int progress = 0;
        string str_progress = "0";
        public List<string> availableitems = new List<string>
        {
            {"pack://application:,,,/img/items/key.png" },
            {"placeholder"},
        };





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



        public void InitInventory()
        {
            inventorySlots = new List<Image> { slot1, slot2, slot3 };
        }

        public void AddItem(int itemId)
        {
            if (inventoryItems.Contains(itemId)) return;

            inventoryItems.Add(itemId);
            int slotIndex = inventoryItems.Count - 1;
            inventorySlots[slotIndex].Source = new BitmapImage(new Uri(availableitems[itemId]));
        }

        public bool HasItem(int itemId)
        {
            return inventoryItems.Contains(itemId);
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

        private Stack<int> history = new();
        private int currentSceneId = 0;

        public void GoToScene(int sceneId)
        {
            LoadScene(sceneId);
        }


        private void LoadScene(int sceneId)
        {
            if (sceneId == 3)
            {
                trezor.Visibility = Visibility.Visible; game.Visibility = Visibility.Collapsed;
            }
            else {
                trezor.Visibility = Visibility.Collapsed;
                game.Visibility = Visibility.Visible;
            }

            currentSceneId = sceneId;
            var scene = sceny.scenes.First(s => s.Id == sceneId);

            pozadi.Source = new BitmapImage(new Uri(scene.BackgroundPath, UriKind.RelativeOrAbsolute));

            foreach (var btn in buttonlist)
                main.Children.Remove(btn);
            buttonlist.Clear();

            foreach (var data in scene.Buttons)
                CreateButton(data);
        }

        private void CreateButton(ButtonData data)
        {
            var button = new Button
            {
                Width = data.Width,
                Height = data.Height,
                Cursor = Cursors.Hand,
                Background = Brushes.White,
                BorderThickness = new Thickness(0),
                Opacity = 0.5,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(data.X, data.Y, 0, 0)
            };

            if (data.IsDialogue) button.Click += (s, e) => LaunchDialogue(data.DialogueId, data.Sleep);
            button.Click += (s, e) => GoToScene(data.TargetSceneId);
            buttonlist.Add(button);
            main.Children.Add(button);
        }












        public void NewGame_Click(object sender, RoutedEventArgs e)
        {
            animace.FadeIn(() =>
            {
                title_menu.Visibility = Visibility.Collapsed;
                pozadi.Source = new BitmapImage(new Uri("pack://application:,,,/img/0/0/backdrop.png"));
                LoadScene(0);
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

        private const string spravnykod = "1234";
        private string enteredCode = "";

        public void Num_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            enteredCode += btn.Tag.ToString();
        }

        public void Unlock_Click(object sender, RoutedEventArgs e)
        {
            if (enteredCode == spravnykod)
            {
                GoToScene(4);
            }
            else
            {
                WrongCode();
            }
        }

        public void WrongCode()
        {
            enteredCode = "";
            MessageBox.Show("Zadal jsi špatný kód.", "Chyba");
        }
    }
}