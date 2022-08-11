class TicTacToeChecker
{
    public bool CheckDraw(char[,] array)
    {
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                if (array[i, j] == Characters.EmptyChar)
                    return false;
            }
        }
        return true;
    }
    public bool CheckWin(char playerChar, char[,] array)
    {
        return CheckDiagonals(playerChar, array) || CheckColumns(playerChar, array) || CheckRows(playerChar, array);
    }
    private bool CheckDiagonals(char playerChar, char[,] array)
    {
        return
        (array[0, 0] == array[1, 1] && array[1, 1] == array[2, 2] && array[0, 0] == playerChar) ||
        (array[0, 2] == array[1, 1] && array[1, 1] == array[2, 0] && array[0, 2] == playerChar);
    }
    private bool CheckColumns(char playerChar, char[,] array)
    {
        for (int j = 0; j < array.GetLength(1); j++)
        {
            if (array[0, j] == array[1, j] && array[1, j] == array[2, j] && array[0, j] == playerChar)
                return true;
        }
        return false;
    }
    private bool CheckRows(char playerChar, char[,] array)
    {
        for (int i = 0; i < array.GetLength(0); i++)
        {
            if (array[i, 0] == array[i, 1] && array[i, 1] == array[i, 2] && array[i, 0] == playerChar)
                return true;
        }
        return false;
    }

}
