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

namespace TicTacToe_Minmax
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private int playrScore = 0;
        private int computerScore = 0;


        private Button[,] buttons;


        private Checker checker;
        private Minimax computer;
        private WindowHelper helper;

        internal MainWindow(Checker checker, WindowHelper helper, Minimax minimax)
        {
            InitializeComponent();
            this.checker = checker;
            this.helper = helper;
            this.computer = minimax;


            buttons = new Button[3, 3];
            buttons[0, 0] = Button0;
            buttons[0, 1] = Button1;
            buttons[0, 2] = Button2;
            buttons[1, 0] = Button3;
            buttons[1, 1] = Button4;
            buttons[1, 2] = Button5;
            buttons[2, 0] = Button6;
            buttons[2, 1] = Button7;
            buttons[2, 2] = Button8;

            UpdateScore();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Play(button);

        }

        private void Play(Button button)
        {
            helper.DisplayCharOnButton(Characters.PlayerChar, button);
            if (checker.IsWin(Characters.PlayerChar, buttons))
            {
                playrScore++;
                UpdateScore();
                ShowMessageAndRestart("Player Won");
            }
            else if (checker.IsDraw(buttons))
            {
                ShowMessageAndRestart("Draw");
            }
            else
            {
                computer.Play(buttons);

                if (checker.IsWin(Characters.ComputerChar, buttons))
                {
                    computerScore++;
                    UpdateScore();
                    ShowMessageAndRestart("Computer Won");
                }
                else if (checker.IsDraw(buttons))
                {
                    ShowMessageAndRestart("Draw");
                }
            }
        }

        private void ShowMessageAndRestart(string Message)
        {
            MessageBoxResult result = MessageBox.Show(Message, "Message Box", MessageBoxButton.OK);
            if (result == MessageBoxResult.OK)
            {
                RestartBoard();
            }
        }

        private void UpdateScore()
        {
            ComputerScoreTextBlock.Text = "Computer " + computerScore;
            HumanScoreTextBlock.Text = "Player " + playrScore;
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            RestartBoard();
        }
        private void RestartBoard()
        {
            for (int i = 0; i < buttons.GetLength(0); i++)
            {
                for (int j = 0; j < buttons.GetLength(1); j++)
                {
                    buttons[i, j].Content = string.Empty;
                    buttons[j, i].IsEnabled = true;
                }
            }
        }

    }
}
