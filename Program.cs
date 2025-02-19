using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Snake
{
    class Program
    {
        // Define an enum for directions
        enum Direction
        {
            UP,
            DOWN,
            LEFT,
            RIGHT
        }

        // Define an enum for game states
        enum GameState
        {
            Running,
            Over
        }

        static void Main(string[] args)
        {
            int screenWidth = Console.WindowWidth;
            int screenHeight = Console.WindowHeight;
            int score = 5;
            Random random = new Random();

            // Initialize Snake's Head
            SnakeHead head = new SnakeHead
            {
                X = screenWidth / 2,
                Y = screenHeight / 2,
                Color = ConsoleColor.Red
            };

            Direction direction = Direction.RIGHT;
            List<int> snakeXPositions = new List<int>();
            List<int> snakeYPositions = new List<int>();
            int berryX = random.Next(1, screenWidth - 1);
            int berryY = random.Next(1, screenHeight - 1);

            DateTime lastMovementTime = DateTime.Now;

            // Initialize game state
            GameState gameState = GameState.Running;

            // Main game loop
            while (gameState == GameState.Running)
            {
                Console.Clear();

                // Draw the borders of the game area
                DrawBorders(screenWidth, screenHeight);

                // Check if snake hits the border
                if (head.X == screenWidth - 1 || head.X == 0 || head.Y == screenHeight - 1 || head.Y == 0)
                {
                    Console.SetCursorPosition(0, 1);
                    Console.WriteLine("Game Over: Snake hit the border.");
                    gameState = GameState.Over; // Update game state
                    break;  // End the game loop
                }

                // Draw the berry on the screen
                DrawBerry(berryX, berryY);

                // Check if snake eats the berry
                if (berryX == head.X && berryY == head.Y)
                {
                    score++;
                    berryX = random.Next(1, screenWidth - 1);
                    berryY = random.Next(1, screenHeight - 1);
                }

                // **Collision detection**: Check if the snake collides with itself before adding the new head
                if (snakeXPositions.Contains(head.X) && snakeYPositions.Contains(head.Y))
                {
                    gameState = GameState.Over; // Update game state
                    break;  // End the game loop
                }

                // Add the new head position to the snake's body
                snakeXPositions.Add(head.X);
                snakeYPositions.Add(head.Y);

                // Remove the last position of the snake's body if it hasn't eaten a berry
                if (snakeXPositions.Count() > score)
                {
                    snakeXPositions.RemoveAt(0);
                    snakeYPositions.RemoveAt(0);
                }

                // Draw the snake's body and the head
                for (int i = 0; i < snakeXPositions.Count(); i++)
                {
                    Console.SetCursorPosition(snakeXPositions[i], snakeYPositions[i]);
                    Console.Write("■");
                }

                // Move the snake based on the current direction
                MoveSnake(ref head, direction);

                // Process user input for movement immediately (without waiting for the next frame)
                HandleUserInput(ref direction);

                // Pause the game for a bit before the next move (100ms delay for faster responsiveness)
                lastMovementTime = DateTime.Now;
                while (true)
                {
                    if (DateTime.Now.Subtract(lastMovementTime).TotalMilliseconds > 100) { break; }
                }
            }

            // Display the final score when the game ends
            if (gameState == GameState.Over)
            {
                Console.SetCursorPosition(screenWidth / 5, screenHeight / 2);
                Console.WriteLine("Game Over. Score: " + score);
                Console.SetCursorPosition(screenWidth / 5, screenHeight / 2 + 1);
                Console.WriteLine("Exiting the game...");
            }
        }

        // Class representing the Snake's Head
        class SnakeHead
        {
            public int X { get; set; }
            public int Y { get; set; }
            public ConsoleColor Color { get; set; }
        }

        // Function to draw the borders of the game area
        static void DrawBorders(int screenWidth, int screenHeight)
        {
            for (int i = 0; i < screenWidth; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("■");
            }

            for (int i = 0; i < screenWidth; i++)
            {
                Console.SetCursorPosition(i, screenHeight - 1);
                Console.Write("■");
            }

            for (int i = 0; i < screenHeight; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("■");
            }

            for (int i = 0; i < screenHeight; i++)
            {
                Console.SetCursorPosition(screenWidth - 1, i);
                Console.Write("■");
            }
        }

        // Function to draw the berry at a random position
        static void DrawBerry(int berryX, int berryY)
        {
            Console.SetCursorPosition(berryX, berryY);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("■");
        }

        // Function to handle user input for snake movement
        static void HandleUserInput(ref Direction direction)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key.Equals(ConsoleKey.UpArrow) && direction != Direction.DOWN)
                {
                    direction = Direction.UP;
                }
                if (key.Key.Equals(ConsoleKey.DownArrow) && direction != Direction.UP)
                {
                    direction = Direction.DOWN;
                }
                if (key.Key.Equals(ConsoleKey.LeftArrow) && direction != Direction.RIGHT)
                {
                    direction = Direction.LEFT;
                }
                if (key.Key.Equals(ConsoleKey.RightArrow) && direction != Direction.LEFT)
                {
                    direction = Direction.RIGHT;
                }
            }
        }

        // Function to move the snake based on the current direction
        static void MoveSnake(ref SnakeHead head, Direction direction)
        {
            switch (direction)
            {
                case Direction.UP:
                    head.Y--;
                    break;
                case Direction.DOWN:
                    head.Y++;
                    break;
                case Direction.LEFT:
                    head.X--;
                    break;
                case Direction.RIGHT:
                    head.X++;
                    break;
            }
        }
    }
}