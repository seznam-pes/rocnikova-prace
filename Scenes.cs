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
        Animations animace;
        public Scenes(MainWindow mainWindow, Animations animacie)
        {
            main = mainWindow;
            animace = animacie;
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
            public int SpecialId;
        }

        public List<Scene> scenes = new()
{
    new Scene
    {
        Id = 0,
        BackgroundPath = "pack://application:,,,/img/0/0/backdrop.png",
        Buttons = new()
        {
            new ButtonData { X = 1228, Y = 809, Width = 70, Height = 200, TargetSceneId = 1 },
            new ButtonData { X = 773, Y = 852, Width = 25, Height = 130, SpecialId = 1 },
            new ButtonData { X = 1585, Y = 850, Width = 160, Height = 150, SpecialId = 4 },
            new ButtonData { X = 368, Y = 809, Width = 222, Height = 200, SpecialId = 6 },
            new ButtonData { X = 1441, Y = 850, Width = 44, Height = 130, SpecialId = 6 },
        }
    },
    new Scene
    {
        Id = 1,
        BackgroundPath = "pack://application:,,,/img/0/1/backdrop.png",
        Buttons = new()
        {
            new ButtonData { X = 1700, Y = 230, Width = 77, Height = 170, TargetSceneId = 0 },
            new ButtonData { X = 830, Y = 540, Width = 99, Height = 184, SpecialId = 8 },
            new ButtonData { X = 300, Y = 620, Width = 123, Height = 155, SpecialId = 101 },
            new ButtonData { X = 600, Y = 600, Width = 111, Height = 111, SpecialId = 102 },
            new ButtonData { X = 1650, Y = 505, Width = 177, Height = 360, SpecialId = 103 },
            new ButtonData { X = 1567, Y = 585, Width = 50, Height = 170, SpecialId = 104 },
            new ButtonData { X = 1414, Y = 730, Width = 160, Height = 400, SpecialId = 105 },
            new ButtonData { X = 1234, Y = 600, Width = 50, Height = 130, SpecialId = 106}, //matka dcery, dodělat
        }
    },
    new Scene
    {
        Id = 2,
        BackgroundPath = "pack://application:,,,/img/0/2/backdrop.png",
        Buttons = new()
        {
            new ButtonData {X = 373, Y = 737, Width = 100, Height = 40, TargetSceneId = 2, SpecialId = 2},
            new ButtonData {X = 50, Y = 465, Width = 100, Height = 130, SpecialId = 3},
            new ButtonData {X = 1718, Y = 326, Width = 134, Height = 610, TargetSceneId = 0 },
            new ButtonData {X = 1300, Y = 515, Width = 107, Height = 100, TargetSceneId = 3 },
        }
    },

    new Scene
    {
        Id = 3,
        BackgroundPath = "pack://application:,,,/img/0/3/dackbrop.png",
        Buttons = new()
        {
            new ButtonData {X = 60, Y = 33, Width = 265, Height = 150, TargetSceneId = 2 },
        }
    },

        new Scene
    {
        Id = 4,
        BackgroundPath = "pack://application:,,,/img/0/4/backdrop.png",
        Buttons = new()
        {
            new ButtonData {X = 85, Y = 46, Width = 350, Height = 222, SpecialId = 5 },
        }
    },

        new Scene
    {
        Id = 5,
        BackgroundPath = "pack://application:,,,/img/0/5/backdrop.png",
        Buttons = new()
        {
            new ButtonData {X = 85, Y = 46, Width = 350, Height = 222, TargetSceneId = 2 },
        }
    },

        new Scene
    {
        Id = 6,
        BackgroundPath = "pack://application:,,,/img/1/0/backdrop.png",
        Buttons = new()
        {
            new ButtonData {X = 760, Y = 1111, Width = 400, Height = 100, TargetSceneId = 0 },
            new ButtonData {X = 567, Y = 650, Width = 255, Height = 350, SpecialId = 7 },
        }
    },
        new Scene
    {
        Id = 7,
        BackgroundPath = "pack://application:,,,/img/1/1/backdrop.png",
        Buttons = new()
        {
            
        }
    },

        new Scene
    {
        Id = 90,
        BackgroundPath = "pack://application:,,,/img/0/90/backdrop.png",
        Buttons = new()
        {
            new ButtonData {X = 85, Y = 46, Width = 350, Height = 222, TargetSceneId = 1 },
        }
    },
};

        public void SwitchChapter(int scene)
        {
            animace.FadeIn(() =>
            {
                main.WriteSave();
                main.LoadScene(scene);
                main.progress++;
                animace.FadeOut();
            });
        }
    }
}
