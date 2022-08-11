class Game
{
    private IDrawer drawer;
    private Checker checker;
    private Minmax computer;
    private char[,] array = new char[3, 3];
    public Game(IDrawer drawer)
    {
        this.drawer = drawer;
        checker = new Checker();
        computer = new Minmax(checker);
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
                if (checker.IsWin(Characters.PlayerChar, array))
                {
                    drawer.DeclareWinner(true);
                    stateWinOrDraw = true;
                    continue;
                }
                if (checker.IsDraw(array))
                {
                    stateWinOrDraw = true;
                    drawer.DeclareDraw();
                    continue;
                }
                else
                {
                    computer.Play(array);
                    drawer.Draw(array);
                    if (checker.IsWin(Characters.ComputerChar, array))
                    {
                        stateWinOrDraw = true;
                        drawer.DeclareWinner(false);
                    }
                }
            }
        }
    }
    private bool PlayerTurn(int[] input)
    {
        if (array[input[0], input[1]] == Characters.EmptyChar)
        {
            array[input[0], input[1]] = Characters.PlayerChar;
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
                array[i, j] = Characters.EmptyChar;
            }
        }
    }
}