public static class Characters
{
    public static char EmptyChar { get; }
    public static char ComputerChar { get; }
    public static char PlayerChar { get; }
    static Characters()
    {
        EmptyChar = ' ';
        ComputerChar = 'o';
        PlayerChar = 'x';
    }

}
