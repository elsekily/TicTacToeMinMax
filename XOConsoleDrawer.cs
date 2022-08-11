public class XOConsoleDrawer : IXODrawer
{
    private int arrayLength;
    public XOConsoleDrawer(int arrayLength = 3)
    {
        this.arrayLength = 3;
    }
    public void DeclareDraw()
    {
        System.Console.WriteLine("DRAW!");
        EndGame();
    }
    public void DeclareWinner(bool playerOrComputer)
    {
        if (playerOrComputer)
            System.Console.WriteLine("player Win!");
        else
            System.Console.WriteLine("computer Win!");

        EndGame();
    }
    private void EndGame()
    {
        Console.ReadKey();
        Console.Clear();
        Console.SetCursorPosition(0, 0);
    }
    public void Draw(char[,] array)
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
                System.Console.Write("\n----------");

            else
                System.Console.Write("\n|--------|");

        }
    }
    public int[] GetInput()
    {
        var input = ((int)Console.ReadKey().KeyChar) - 49;
        if (input < 0 || input > 8)
        {
            System.Console.WriteLine("Please input a valid position.\nPress ENTER to try again!");
            Console.ReadKey();
            return GetInput();
        }
        var i = input / arrayLength;
        var j = input % arrayLength;
        return new int[] { i, j };
    }
}
