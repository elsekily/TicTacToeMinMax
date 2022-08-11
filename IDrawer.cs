interface IDrawer
{
    void Draw(char[,] array);
    void DeclareWinner(bool playerOrComputer);
    void DeclareDraw();
    int[] GetInput();
}
