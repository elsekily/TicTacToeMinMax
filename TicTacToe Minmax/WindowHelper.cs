using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe_Minmax
{
    internal class WindowHelper
    {
        public void DisplayCharOnButton(char character, Button button)
        {
            button.Content = character;
            button.FontSize = 24;
            button.FontFamily = new FontFamily("Arial Black");
            button.FontWeight = FontWeights.Bold;

            button.IsEnabled = false;
        }
    }
}
