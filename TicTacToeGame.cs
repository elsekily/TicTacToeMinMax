// See https://aka.ms/new-console-template for more information
class TicTacToeGame
{
    private char[,] array = new char[3, 3];
    private char computerChar = 'o';
    private char playerChar = 'x';
    private char emptyChar = ' ';
    public TicTacToeGame()
    {
        PopulateArray();
        Drawer();
        var stateWinOrDraw = false;
        while (!stateWinOrDraw)
        {
            var input = ((int)Console.ReadKey().KeyChar) - 49;
            if (!PlayerTurn(input))
            {
                System.Console.WriteLine("input valid pos press ENTER to try again!");
                Console.ReadKey();
            }
            else
            {
                Drawer();
                if (CheckWin(playerChar))
                {
                    System.Console.WriteLine("player Win!");
                    stateWinOrDraw = true;
                    continue;
                }
                if (CheckDraw())
                {
                    stateWinOrDraw = true;
                    System.Console.WriteLine("DRAW!");
                    continue;
                }
                else
                {
                    ComputerTurn();
                    Drawer();
                    if (CheckWin('o'))
                    {
                        stateWinOrDraw = true;
                        System.Console.WriteLine("computer WIn");
                    }
                }
            }
        }
        Console.ReadKey();
        Console.Clear();
        Console.SetCursorPosition(0, 0);
    }
    private class Move
    {
        public int I { get; set; }
        public int J { get; set; }
        public int Score { get; set; }
    }
    private void ComputerTurn()
    {
        if (array[1, 1] == emptyChar)
        {
            array[1, 1] = computerChar;
            return;
        }
        var moves = new List<Move>();
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                if (array[i, j] == emptyChar)
                {
                    array[i, j] = computerChar;
                    if (CheckWin(computerChar))
                        return;
                    //Drawer();
                    moves.Add(new Move() { I = i, J = j, Score = GetBestMove(true) });
                    array[i, j] = emptyChar;
                    //Drawer();
                }
            }
        }

        SetNextMove(moves);
    }
    private void SetNextMove(List<Move> moves)
    {
        moves = moves.OrderByDescending(m => m.Score).ToList();
        var i = 1;
        while (i < moves.Count)
        {
            if (moves[i].Score != moves[0].Score)
                moves.Remove(moves[i]);
            else
                i++;
        }
        if (moves.Count == 1)
            array[moves[0].I, moves[0].J] = computerChar;
        else
        {
            var rand = new Random();
            var index = rand.Next(0, moves.Count);
            array[moves[index].I, moves[index].J] = computerChar;
        }

    }
    private int GetBestMove(bool playerTurn)
    {
        var score = new List<int>();
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                if (array[i, j] == emptyChar)
                {
                    array[i, j] = playerTurn ? playerChar : computerChar;
                    if (CheckWin(playerChar))
                    {
                        array[i, j] = emptyChar;
                        return -1;
                    }
                    else if (CheckWin(computerChar))
                    {
                        array[i, j] = emptyChar;
                        return 1;
                    }

                    else if (CheckDraw())
                    {
                        array[i, j] = emptyChar;
                        return 0;
                    }
                    else
                    {
                        score.Add(GetBestMove(!playerTurn));
                        array[i, j] = emptyChar;
                    }
                }
            }
        }
        return score.Max();
    }
    private bool PlayerTurn(int input)
    {
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                if (input == 0 && array[i, j] == emptyChar)
                {
                    array[i, j] = playerChar;
                    return true;
                }
                else if (input == 0 && array[i, j] != emptyChar)
                {
                    return false;
                }
                input--;
            }
        }
        return false;
    }
    private void PopulateArray()
    {
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                array[i, j] = emptyChar;
            }
        }
    }
    private bool CheckDraw()
    {
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                if (array[i, j] == emptyChar)
                    return false;
            }
        }
        return true;
    }
    private bool CheckWin(char playerChar)
    {
        return CheckDiagonals(playerChar) || CheckColumns(playerChar) || CheckRows(playerChar);
    }
    private bool CheckDiagonals(char playerChar)
    {
        return
        (array[0, 0] == array[1, 1] && array[1, 1] == array[2, 2] && array[0, 0] == playerChar) ||
        (array[0, 2] == array[1, 1] && array[1, 1] == array[2, 0] && array[0, 2] == playerChar);
    }
    private bool CheckColumns(char playerChar)
    {
        for (int j = 0; j < array.GetLength(1); j++)
        {
            if (array[0, j] == array[1, j] && array[1, j] == array[2, j] && array[0, j] == playerChar)
                return true;
        }
        return false;
    }
    private bool CheckRows(char playerChar)
    {
        for (int i = 0; i < array.GetLength(0); i++)
        {
            if (array[i, 0] == array[i, 1] && array[i, 1] == array[i, 2] && array[i, 0] == playerChar)
                return true;
        }
        return false;
    }
    private void Drawer()
    {

        Console.Clear();
        Console.SetCursorPosition(0, 0);
        System.Console.Write("----------");
        for (int i = 0; i < array.GetLength(0); i++)
        {
            System.Console.Write("\n|");
            for (int j = 0; j < array.GetLength(1); j++)
            {
                Console.Write(" {0}|", array[i, j]);
            }
            if (i == array.GetLength(0) - 1)
            {
                System.Console.Write("\n----------");

            }
            else
                System.Console.Write("\n|--------|");

        }
    }
}