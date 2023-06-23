using System.Windows.Controls;

namespace TicTacToe_Minmax
{
    internal class Checker
    {
        public bool IsDraw(Button[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j].Content.ToString() == string.Empty)
                        return false;
                }
            }
            return true;
        }
        public bool IsWin(char playerChar, Button[,] array)
        {
            return CheckDiagonals(playerChar, array) || CheckColumns(playerChar, array) || CheckRows(playerChar, array);
        }
        private bool CheckDiagonals(char playerChar, Button[,] array)
        {
            return
            (array[0, 0].Content.Equals(array[1, 1].Content) && array[1, 1].Content.Equals(array[2, 2].Content) && CompareStringToChar(array[0, 0].Content.ToString(),playerChar)) ||
            (array[0, 2].Content.Equals(array[1, 1].Content) && array[1, 1].Content.Equals(array[2, 0].Content) && CompareStringToChar(array[0, 2].Content.ToString(), playerChar));
        }
        private bool CheckColumns(char playerChar, Button[,] array)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                if (array[0, j].Content.Equals(array[1, j].Content) && array[1, j].Content.Equals(array[2, j].Content) && CompareStringToChar(array[0, j].Content.ToString(),playerChar))
                    return true;
            }
            return false;
        }
        private bool CheckRows(char playerChar, Button[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                if (array[i, 0].Content.Equals(array[i, 1].Content) && array[i, 1].Content.Equals(array[i, 2].Content) && CompareStringToChar(array[i, 0].Content.ToString(),playerChar))
                    return true;
            }
            return false;
        }
        public bool CompareStringToChar(string input, char character)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            return input[0] == character;
        }

    }
}
