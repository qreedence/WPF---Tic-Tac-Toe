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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppEden
{
    /// <summary>
    /// Interaction logic for StartScreen.xaml
    /// </summary>
    public partial class StartScreen : Page
    {
        public StartScreen()
        {
            InitializeComponent();
        }

        private void StartGame(object sender, RoutedEventArgs e)
        {
            EnterName();
        }

        public void EnterName()
        {
            StartGameButton.Visibility = Visibility.Collapsed;
            NameField.Visibility = Visibility.Visible;
            EnterNameButton.Visibility = Visibility.Visible;
        }

        private void SubmitName(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(NameField.Text))
            {
                var button = sender as Wpf.Ui.Controls.Button;
                if (button != null)
                {
                    var storyboard = new Storyboard();
                    var translateTransform = new TranslateTransform();
                    button.RenderTransform = translateTransform;
                    var animation = new DoubleAnimation
                    {
                        From = 0,
                        To = 10,
                        Duration = new Duration(TimeSpan.FromMilliseconds(100)),
                        AutoReverse = true,
                        RepeatBehavior = new RepeatBehavior(2)
                    };
                    Storyboard.SetTarget(animation, button);
                    Storyboard.SetTargetProperty(animation, new PropertyPath("RenderTransform.X"));
                    storyboard.Children.Add(animation);
                    storyboard.Begin();
                }
                return;
            }
            GameState.PlayerName = NameField.Text;
            this.NavigationService.Navigate(new Game());
        }
    }
}
