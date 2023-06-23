using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace TicTacToe_Minmax
{
    internal class Minimax
    {
        private Checker checker;
        private WindowHelper helper;

        public Minimax(Checker checker,WindowHelper helper)
        {
            this.checker = checker;
            this.helper = helper;
        }
        private class Move
        {
            public int I { get; set; }
            public int J { get; set; }
            public int Score { get; set; }
        }
        public void Play(Button[,] array)
        {
            var moves = new List<Move>();
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j].Content.ToString() == "")
                    {
                        array[i, j].Content = Characters.ComputerChar;
                        if (checker.IsWin(Characters.ComputerChar, array))
                            return;

                        moves.Add(new Move() { I = i, J = j, Score = CheckAllPossibleMoves(true, array) });
                        array[i, j].Content = "";
                    }
                }
            }

            SetNextMove(moves, array);
        }
        private int CheckAllPossibleMoves(bool playerTurn, Button[,] array)
        {
            var score = new List<int>();
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j].Content.ToString() == "")
                    {
                        array[i, j].Content = playerTurn ? Characters.PlayerChar : Characters.ComputerChar;
                        if (checker.IsWin(Characters.PlayerChar, array))
                        {
                            array[i, j].Content = "";
                            return -1;
                        }
                        else if (checker.IsWin(Characters.ComputerChar, array))
                        {
                            array[i, j].Content = "";
                            return 1;
                        }

                        else if (checker.IsDraw(array))
                        {
                            array[i, j].Content = "";
                            return 0;
                        }
                        else
                        {
                            score.Add(CheckAllPossibleMoves(!playerTurn, array));
                            array[i, j].Content = "";
                        }
                    }
                }
            }
            return playerTurn ? score.Min() : score.Max();
        }
        private void SetNextMove(List<Move> moves, Button[,] array)
        {
            moves = moves.OrderByDescending(m => m.Score).ToList();
            moves = moves.Where(m => m.Score == moves[0].Score).ToList();
            if (moves.Count == 1)
                helper.DisplayCharOnButton(Characters.ComputerChar,array[moves[0].I, moves[0].J]);
            else
            {
                var rand = new Random();
                var index = rand.Next(0, moves.Count);
                helper.DisplayCharOnButton(Characters.ComputerChar, array[moves[index].I, moves[index].J]);
            }
        }
    }
}
