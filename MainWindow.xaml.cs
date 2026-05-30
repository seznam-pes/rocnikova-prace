using Microsoft.VisualBasic.Devices;
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

using Keyboard = System.Windows.Input.Keyboard;

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
            sceny = new Scenes(this, animace);

            this.KeyDown += (s, e) =>
            {
                if (e.Key == Key.Enter)
                {
                    EnterPressed();
                }
            };
        }

        Animations animace;
        Scenes sceny;

        public List<Button> buttonlist = new List<Button>();
        public int currentSceneId = 0;
        public int progress = 0;
        string str_progress = "0";

        public bool hasKey = false;
        public bool trezorLock = true;
        public bool letterRead = false;
        public bool alreadySwitched1 = false;
        public bool barmanTalk = false;
        public bool clownTalk = false;

        private const string spravnykod = "26643";
        private string enteredCode = "";



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




        public async Task LaunchDialogue(string promptedtext, int sleep)
        {
            dialoguebg.Visibility = Visibility.Visible;
            dialoguebar.Visibility = Visibility.Visible;
            dialoguehelp.Visibility = Visibility.Visible;
            dialoguebar.Text = promptedtext;
            await Task.Delay(sleep*1000);
            dialoguebg.Visibility = Visibility.Hidden;
            dialoguebar.Visibility = Visibility.Hidden;
            dialoguehelp.Visibility = Visibility.Hidden;
            dialoguebar.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE8CA00"));
        }

        public void LoadScene(int sceneId)
        {
            if (sceneId == 3 && trezorLock == false)
            {
                LoadScene(4);
            }

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

            pozadi.Source = new BitmapImage(new Uri(scene.BackgroundPath));

            foreach (var btn in buttonlist)
                main.Children.Remove(btn);
            buttonlist.Clear();

            foreach (var data in scene.Buttons)
                CreateButton(data);

            progress = sceneId;
            WriteSave();
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

            button.Click += (s, e) => LoadScene(data.TargetSceneId);
            button.Click += (s, e) => SpecialFunctions(data);
            button.Opacity = 0;
            button.MouseEnter += (s, e) => button.Opacity = 0.5;
            button.MouseLeave += (s, e) => button.Opacity = 0;
            buttonlist.Add(button);
            main.Children.Add(button);
        }

        public async Task SpecialFunctions(ButtonData data)
        {
            if (data.SpecialId == 1)
            {
                if (hasKey == true)
                {
                    LoadScene(2);
                }
                else
                {
                    await LaunchDialogue("A sakra, je to zamčené. Potřebuji klíč.", 5);
                }
            }

            else if (data.SpecialId == 2)
            {
                await LaunchDialogue("angd... hmm, co to může znamenat?", 7);
            }

            else if (data.SpecialId == 3)
            {
                LoadScene(5);
                await LaunchDialogue("Co to je za nástroj? To neznám...", 4);
            }
            else if (data.SpecialId == 4)
            {
                if (letterRead == true)
                {
                    if (alreadySwitched1 == true)
                    {
                        LoadScene(6);
                    }
                    else
                    {
                        sceny.SwitchChapter(6);
                        alreadySwitched1 = true;
                    }
                }
                else
                {
                    await LaunchDialogue("Proč bych tam chodil? Zatím tam nic nenajdu.", 5);
                }
            }
            else if (data.SpecialId == 5)
            {
                LoadScene(2);
                await LaunchDialogue("Aha! Takže do lesa zmizela... Musím tam!", 5);
                letterRead = true;
            }
            else if (data.SpecialId == 6)
            {
                await LaunchDialogue("Ne. Tudy cesta nevede.", 3);
            }

            else if (data.SpecialId == 7)
            {
                LoadScene(7);
                await LaunchDialogue("Zdá se, že tohle je vše co autor hry stihl udělat. Tak dnes tu záhadu asi nevyřeším.", 10);
                await Task.Delay(15000);
                EnterPressed();
            }
            else if (data.SpecialId == 8)
            {
                barmanTalk = true;
                dialoguebar.FontSize = 50;
                LoadScene(1);
                await LaunchDialogue("Ahoj! Hm, Tebe neznám. Ty jsi tu určitě kvůli Anežce, té zmizelé dívce, že? Někde by tu měla být její matka, zkus ji najít a třeba ti mohla pomoct.", 12);
                dialoguebar.FontSize = 72;
            }
            else if (data.SpecialId == 101)
            {
                LoadScene(1);
                await LaunchDialogue("Co tu vokouníš?! Nevidíš, že zde vedeme debatu. Odpal.", 5);
            }
            else if (data.SpecialId == 102)
            {
                LoadScene(1);
                await LaunchDialogue("Ještě jsem na nic nepřišel, ale už mám docela žízeň. Ne, nejprv povinnosti, potom zábava.", 5);
            }
            else if (data.SpecialId == 103)
            {
                clownTalk = true;
                LoadScene(1);
                await LaunchDialogue("Král do boje táh, do veliké dálky...", 5);
            }
            else if (data.SpecialId == 104)
            {
                LoadScene(1);
                await LaunchDialogue("Ten muzikant je zvláštní, bojím se ho. Něco mi na něm nehraje...", 5);
            }
            else if (data.SpecialId == 105)
            {
                LoadScene(1);
                await LaunchDialogue("Na zdraví!", 3);
            }
            else if (data.SpecialId == 106)
            {
                if (!barmanTalk || !clownTalk)
                { 
                    dialoguebar.FontSize = 50;
                    LoadScene(1);
                    await LaunchDialogue("(Dáma nevypadá, že by si chtěla povídat. Tip: Zkus si prvně promluvit s ostatními postavami.)", 10);
                    dialoguebar.FontSize = 72;
                }
                else
                {
                    hasKey = true;
                    dialoguebar.FontSize = 50;
                    LoadScene(90);
                    await LaunchDialogue("Pozdrav Pánbůh, vy budete ten detektiv, že? Prosím, pomozte mi najít mou dceru! Zde je klíč od mého domu, třeba v něm něco najdete.", 15);
                    dialoguebar.FontSize = 72;
                }
            }
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
            LaunchDialogue("Slyšel jsem, že se z této vesnice ze dne na den vypařila dívka. Musím ji najít!", 10);
        }

        public void Launch_Click(object sender, RoutedEventArgs e)
        {
            ReadSave();
            animace.FadeIn(() =>
            {
                title_menu.Visibility = Visibility.Collapsed;
                LoadScene(progress);
                game.Visibility = Visibility.Visible;
                animace.FadeOut();
            });
        }

        public void Continue_Click(object sender, RoutedEventArgs e)
        {
            pause_menu.Visibility = Visibility.Collapsed;
            LoadScene(currentSceneId);
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
                        LoadScene(progress);
                    }
                    else
                    {
                        progress = currentSceneId;
                        WriteSave();
                        foreach (var btn in buttonlist)
                            main.Children.Remove(btn);
                        buttonlist.Clear();

                        pause_menu.Visibility = Visibility.Visible;
                        game.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }

        public void EnterPressed()
        {
            if (currentSceneId == 7)
            {
                progress = 0;
                WriteSave();
                Rectangle end = new Rectangle();
                end.Fill = Brushes.Black;
                end.HorizontalAlignment = HorizontalAlignment.Stretch;
                end.VerticalAlignment = VerticalAlignment.Stretch;
                main.Children.Add(end);

                TextBlock thx = new TextBlock();
                thx.Foreground = Brushes.White;
                thx.HorizontalAlignment = HorizontalAlignment.Center;
                thx.VerticalAlignment = VerticalAlignment.Center;
                thx.Text = "Děkuji za zahrání! Byť jsi to dokončil za asi 3 minuty...";
                thx.FontFamily = new FontFamily("Juice ITC");
                thx.FontWeight = FontWeights.Bold;
                thx.TextWrapping = TextWrapping.Wrap;
                thx.FontSize = 72;
                thx.TextAlignment = TextAlignment.Center;
                thx.Width = 450;
                thx.Visibility = Visibility.Visible;
                main.Children.Add(thx);
            }
            else
            {
                dialoguebg.Visibility = Visibility.Hidden;
                dialoguebar.Visibility = Visibility.Hidden;
                dialoguehelp.Visibility = Visibility.Hidden;
            }
        }






        public void Num_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            enteredCode += btn.Tag.ToString();
        }

        public void Unlock_Click(object sender, RoutedEventArgs e)
        {
            if (enteredCode == spravnykod)
            {
                LoadScene(4);
                trezorLock = false;
            }
            else
            {
                WrongCode();
            }
        }

        public void WrongCode()
        {
            enteredCode = "";
            MessageBox.Show("Zadal jsi špatný kód. Zkus to znovu. (Nápověda: Kód má 5 čísel)", "Chyba");
        }
    }
}