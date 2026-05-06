using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace WpfApp1
{
    public class Scenes
    {
        MainWindow main;
        Dialogues dialogy;
        Animations animace;
        public Scenes(MainWindow mainWindow, Dialogues dialogues, Animations animacie)
        {
            main = mainWindow;
            animace = animacie;
            dialogy = dialogues;
        }
        public void SwitchScene()
        {
            animace.FadeIn();
            main.buttonlist.Clear();
            main.progress++;
            main.WriteSave();
        }

        public void Scene1()
        {
            SwitchScene();
            main.pozadi.Source = new BitmapImage(new Uri("pack://application:,,,/img/0/backdrop.png"));
            animace.FadeOut();
        }
    }
}
