using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Snake
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var game = new SnakeGame(Console.WindowWidth, Console.WindowHeight);
            var lastMovementTime = DateTime.Now;

            while (game.IsRunning)
            {
                game.Update();

                while (DateTime.Now.Subtract(lastMovementTime).TotalMilliseconds <= 100) { }
                lastMovementTime = DateTime.Now;
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
        
    }
}