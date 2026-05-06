using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace WpfApp1
{
    public class Animations
    {
        MainWindow main;

        public Animations(MainWindow mainWindow)
        {
            main = mainWindow;
        }

        public void FadeIn(Action? onComplete = null)
        {
            main.rect.Visibility = Visibility.Visible;
            main.rect.Opacity = 0;

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

            main.rect.BeginAnimation(UIElement.OpacityProperty, anim);
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
                main.rect.Visibility = Visibility.Collapsed;
            };

            main.rect.BeginAnimation(UIElement.OpacityProperty, anim);
        }
    }
}
