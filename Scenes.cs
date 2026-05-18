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

        public class Scene
        {
            public int Id { get; set; }
            public string BackgroundPath { get; set; }
            public List<ButtonData> Buttons { get; set; } = new();
        }

        public class ButtonData
        {
            public int X, Y, Width, Height;
            public int TargetSceneId;
            public bool IsDialogue = false;
            public int DialogueId;          
            public int Sleep;
        }

        public List<Scene> scenes = new()
{
    new Scene
    {
        Id = 0,
        BackgroundPath = "pack://application:,,,/img/0/0/backdrop.png",
        Buttons = new()
        {
            new ButtonData { X = 1228, Y = 769, Width = 70, Height = 200, TargetSceneId = 1 },
            new ButtonData { X = 773, Y = 792, Width = 25, Height = 130, TargetSceneId = 2 },
        }
    },
    new Scene
    {
        Id = 1,
        BackgroundPath = "pack://application:,,,/img/0/1/backdrop.png",
        Buttons = new()
        {
            new ButtonData { X = 1248, Y = 183, Width = 57, Height = 133, TargetSceneId = 0 },
        }
    },
    new Scene
    {
        Id = 2,
        BackgroundPath = "pack://application:,,,/img/0/2/backdrop.png",
        Buttons = new()
        {
            new ButtonData {X = 1258, Y = 241, Width = 94, Height = 484, TargetSceneId = 0 },
            new ButtonData {X = 953, Y = 395, Width = 67, Height = 85, TargetSceneId = 3 },
        }
    },

    new Scene
    {
        Id = 3,
        BackgroundPath = "pack://application:,,,/img/0/3/dackbrop.png",
        Buttons = new()
        {
            new ButtonData {X = 30, Y = 312, Width = 90, Height = 90, TargetSceneId = 2 },
        }
    },
};

        public void SwitchScene()
        {
            animace.FadeIn();
            main.buttonlist.Clear();
            main.progress++;
            main.WriteSave();
        }
    }
}
