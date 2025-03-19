using static System.Console;

namespace Snake
{
    class Program
    {
        static void Main()
        {
            WindowHeight = 16;
            WindowWidth = 32;

            var game = new Game(WindowWidth, WindowHeight);
            game.Run();
        }
    }
}
