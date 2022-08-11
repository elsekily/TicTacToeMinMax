using System;
class Minmax
{
    private TicTacToeChecker checker;
    public Minmax(TicTacToeChecker checker)
    {
        this.checker = checker;
    }
    private class Move
    {
        public int I { get; set; }
        public int J { get; set; }
        public int Score { get; set; }
    }
    public void Play(char[,] array)
    {
        var moves = new List<Move>();
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                if (array[i, j] == Characters.EmptyChar)
                {
                    array[i, j] = Characters.ComputerChar;
                    if (checker.CheckWin(Characters.ComputerChar, array))
                        return;

                    moves.Add(new Move() { I = i, J = j, Score = CheckAllPossibleMoves(true, array) });
                    array[i, j] = Characters.EmptyChar;
                }
            }
        }

        SetNextMove(moves, array);
    }
    private int CheckAllPossibleMoves(bool playerTurn, Char[,] array)
    {
        var score = new List<int>();
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                if (array[i, j] == Characters.EmptyChar)
                {
                    array[i, j] = playerTurn ? Characters.PlayerChar : Characters.ComputerChar;
                    if (checker.CheckWin(Characters.PlayerChar, array))
                    {
                        array[i, j] = Characters.EmptyChar;
                        return -1;
                    }
                    else if (checker.CheckWin(Characters.ComputerChar, array))
                    {
                        array[i, j] = Characters.EmptyChar;
                        return 1;
                    }

                    else if (checker.CheckDraw(array))
                    {
                        array[i, j] = Characters.EmptyChar;
                        return 0;
                    }
                    else
                    {
                        score.Add(CheckAllPossibleMoves(!playerTurn, array));
                        array[i, j] = Characters.EmptyChar;
                    }
                }
            }
        }
        return score.Sum();
    }
    private void SetNextMove(List<Move> moves, char[,] array)
    {
        moves = moves.OrderByDescending(m => m.Score).ToList();
        moves = moves.Where(m => m.Score == moves[0].Score).ToList();
        if (moves.Count == 1)
            array[moves[0].I, moves[0].J] = Characters.ComputerChar;
        else
        {
            var rand = new Random();
            var index = rand.Next(0, moves.Count);
            array[moves[index].I, moves[index].J] = Characters.ComputerChar;
        }
    }
}
