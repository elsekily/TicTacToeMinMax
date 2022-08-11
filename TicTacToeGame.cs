class TicTacToeGame
{
    private IXODrawer drawer;
    private char[,] array = new char[3, 3];
    private char computerChar = 'o';
    private char playerChar = 'x';
    private char emptyChar = ' ';
    public TicTacToeGame(IXODrawer drawer)
    {
        this.drawer = drawer;
        PopulateArray();
        StartGame();
    }
    private void StartGame()
    {
        drawer.Draw(array);
        var stateWinOrDraw = false;
        while (!stateWinOrDraw)
        {
            if (PlayerTurn(drawer.GetInput()))
            {
                drawer.Draw(array);
                if (CheckWin(playerChar))
                {
                    drawer.DeclareWinner(true);
                    stateWinOrDraw = true;
                    continue;
                }
                if (CheckDraw())
                {
                    stateWinOrDraw = true;
                    drawer.DeclareDraw();
                    continue;
                }
                else
                {
                    ComputerTurn();
                    drawer.Draw(array);
                    if (CheckWin(computerChar))
                    {
                        stateWinOrDraw = true;
                        drawer.DeclareWinner(false);
                    }
                }
            }
        }

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
    private bool PlayerTurn(int[] input)
    {
        if (array[input[0], input[1]] == emptyChar)
        {
            array[input[0], input[1]] = playerChar;
            return true;
        }
        else
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
}