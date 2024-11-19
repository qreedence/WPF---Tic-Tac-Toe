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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.Ui.Controls;

namespace WpfAppEden
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Page
    {
        public List<string> PlayerTiles = new List<string>();
        public List<string> CpuTiles = new List<string>();
        List<Wpf.Ui.Controls.Button> buttons = new List<Wpf.Ui.Controls.Button>();
        private bool isInitialized = false;


        public Game()
        {
            InitializeComponent();
            PlayerNameTextBox.Text = GameState.PlayerName;
            PlayerScoreTextBox.Text = GameState.PlayerScore.ToString();
            CpuScoreTextBox.Text = GameState.CpuScore.ToString();

            buttons.AddRange(new List<Wpf.Ui.Controls.Button> { A1, A2, A3, B1, B2, B3, C1, C2, C3 });
        }


        private void ClaimTile(object sender, RoutedEventArgs e)
        {
            Wpf.Ui.Controls.Button clickedButton = sender as Wpf.Ui.Controls.Button;

            if (clickedButton != null)
            {
                if (PlayerTiles.Contains(clickedButton.Name) || CpuTiles.Contains(clickedButton.Name))
                {
                    return;
                }
                PlayerTiles.Add(clickedButton.Name);
                PlayerTilesText.Text = "";
                clickedButton.Background = new SolidColorBrush((Color.FromRgb(104, 74, 248)));
            }
            EndTurn(CheckWinCondition(), TurnEnder.Player);
        }

        public void CpuClaimsTile()
        {
            var buttonOptions = new List<Wpf.Ui.Controls.Button>();
            foreach (var button in buttons)
            {
                if (!PlayerTiles.Contains(button.Name) && !CpuTiles.Contains(button.Name))
                {
                    buttonOptions.Add(button);
                }
            }
            var random = new Random();
            var selectedButton = buttonOptions[random.Next(buttonOptions.Count)];
            CpuTiles.Add(selectedButton.Name);
            selectedButton.Background = new SolidColorBrush((Color.FromRgb(255, 201, 14)));
            EndTurn(CheckWinCondition(), TurnEnder.CPU);
        }

        public WinState CheckWinCondition()
        {
            foreach (var combination in GameState.WinningCombinations)
            {
                if (combination.All(tile => PlayerTiles.Contains(tile)))
                {
                    return WinState.PlayerWins;
                }
                if (combination.All(tile => CpuTiles.Contains(tile)))
                {
                    return WinState.CpuWins;
                }
            }
            if (PlayerTiles.Count + CpuTiles.Count == 9)
            {
                return WinState.Draw;
            }
            return WinState.InSession;
        }

        public void EndTurn(WinState winState, TurnEnder turnEnder)
        {
            if (winState == WinState.InSession)
            {
                if (turnEnder == TurnEnder.Player)
                {
                    CpuClaimsTile();
                }
            }
            else
            {
                GameOver(winState);
            }
        }

        public void GameOver(WinState winState)
        {
            foreach (var button in buttons)
            {
                button.Visibility = Visibility.Collapsed;
            }
            ResetButton.Visibility = Visibility.Collapsed;

            GameOverScreen.Visibility = Visibility.Visible;
            if (winState == WinState.PlayerWins)
            {
                GameState.PlayerScore++;
                PlayerScoreTextBox.Text = GameState.PlayerScore.ToString();
                GameOverText.Text = $"{GameState.PlayerName} wins!";
            }
            if (winState == WinState.CpuWins)
            {
                GameState.CpuScore++;
                CpuScoreTextBox.Text = GameState.CpuScore.ToString();
                GameOverText.Text = "CPU wins!";
            }
            if (winState == WinState.Draw)
            {
                GameOverText.Text = "Draw!";
            }
        }

        private void PlayAgain(object sender, RoutedEventArgs e)
        {
            PlayerTiles.Clear();
            CpuTiles.Clear();
            GameOverScreen.Visibility = Visibility.Collapsed;
            foreach (var button in buttons)
            {
                button.Background = Brushes.Transparent;
                button.Visibility = Visibility.Visible;
            }
            ResetButton.Visibility = Visibility.Visible;
        }

        private void Reset(object sender, RoutedEventArgs e)
        {
            PlayerTiles = new List<string>();
            CpuTiles = new List<string>();
            foreach (var button in buttons)
            {
                button.Background = Brushes.Transparent;
            }
        }

        private void TileHover(object sender, MouseEventArgs e)
        {
            Wpf.Ui.Controls.Button clickedButton = sender as Wpf.Ui.Controls.Button;
            if (PlayerTiles.Contains(clickedButton.Name) || CpuTiles.Contains(clickedButton.Name))
            {
                return;
            }
            else
            {
                clickedButton.MouseOverBackground = new SolidColorBrush { Color = Color.FromRgb(159, 162, 189), Opacity = 0.7};
            }
        }

        private void TileHoverLeave(object sender, MouseEventArgs e)
        {
            Wpf.Ui.Controls.Button clickedButton = sender as Wpf.Ui.Controls.Button;
            if (PlayerTiles.Contains(clickedButton.Name) || CpuTiles.Contains(clickedButton.Name))
            {
                clickedButton.MouseOverBackground = clickedButton.Background;
            }
            else
            {
                return;
            }
        }
    }
}
