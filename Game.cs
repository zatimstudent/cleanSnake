using System;
using System.Collections.Generic;
using System.Diagnostics;
using static System.Console;

namespace Snake
{
    public class Game
    {
        private int WindowWidth;
        private int WindowHeight;
        private Random rand;
        private int score;
        private Pixel head;

        private List<IBerry> berries;

        private List<Pixel> body;
        private Direction currentMovement;
        private bool gameover;
        private BerryFactory berryFactory;
        private string gameOverReason;

        private int poisonSpawnCounter;
        private int poisonSpawnThreshold;

        public Game(int width, int height)
        {
            WindowWidth = width;
            WindowHeight = height;
            rand = new Random();
            score = 5;
            head = new Pixel(WindowWidth / 2, WindowHeight / 2, ConsoleColor.Red);
            berryFactory = new BerryFactory();

            berries = new List<IBerry>();
            berries.Add(
                berryFactory.CreateDefinedBerry(BerryType.Regular, WindowWidth, WindowHeight)
            );

            poisonSpawnCounter = 0;
            poisonSpawnThreshold = rand.Next(1, 6);

            body = new List<Pixel>();
            currentMovement = Direction.Right;
            gameover = false;
        }

        public void Run()
        {
            while (true)
            {
                Clear();

                gameover |= (
                    head.XPos == WindowWidth - 1
                    || head.XPos == 0
                    || head.YPos == WindowHeight - 1
                    || head.YPos == 0
                );

                Renderer.DrawBorder(WindowWidth, WindowHeight);

                for (int i = 0; i < body.Count; i++)
                {
                    Renderer.DrawPixel(body[i]);
                    if (body[i].XPos == head.XPos && body[i].YPos == head.YPos)
                    {
                        gameover = true;
                    }
                }

                if (gameover)
                    break;

                for (int i = 0; i < berries.Count; i++)
                {
                    IBerry berry = berries[i];
                    Renderer.DrawPixel(berry.GetPixel());

                    if (berry.XPos == head.XPos && berry.YPos == head.YPos)
                    {
                        berry.OnEaten(this);

                        berries.Clear();

                        if (berry is RegularBerry)
                        {
                            berries.Add(
                                berryFactory.CreateDefinedBerry(
                                    BerryType.Regular,
                                    WindowWidth,
                                    WindowHeight
                                )
                            );
                            poisonSpawnCounter++;

                            if (poisonSpawnCounter >= poisonSpawnThreshold)
                            {
                                berries.Add(
                                    berryFactory.CreateDefinedBerry(
                                        BerryType.Poison,
                                        WindowWidth,
                                        WindowHeight
                                    )
                                );
                                poisonSpawnCounter = 0;
                                poisonSpawnThreshold = rand.Next(1, 6);
                            }
                        }
                        else if (berry is PoisonBerry)
                        {
                            berries.Add(
                                berryFactory.CreateDefinedBerry(
                                    BerryType.Regular,
                                    WindowWidth,
                                    WindowHeight
                                )
                            );
                        }
                        break;
                    }
                }

                if (gameover)
                    break;

                Renderer.DrawPixel(head);

                var sw = Stopwatch.StartNew();
                while (sw.ElapsedMilliseconds <= 700)
                {
                    currentMovement = ReadMovement(currentMovement);
                }

                body.Add(new Pixel(head.XPos, head.YPos, ConsoleColor.Green));

                switch (currentMovement)
                {
                    case Direction.Up:
                        head.YPos--;
                        break;
                    case Direction.Down:
                        head.YPos++;
                        break;
                    case Direction.Left:
                        head.XPos--;
                        break;
                    case Direction.Right:
                        head.XPos++;
                        break;
                }

                if (body.Count > score)
                {
                    body.RemoveAt(0);
                }
            }

            Renderer.ShowGameOver(score, WindowWidth, WindowHeight);
            ReadKey();
        }

        public void IncreaseScore(int amount)
        {
            score += amount;
        }

        public void DecreaseScore(int amount)
        {
            score -= amount;

            if (score < 0)
            {
                score = 0;
                EndGame("You died to poison");
            }
        }

        public void EndGame(string reason = null)
        {
            gameover = true;
            gameOverReason = reason ?? "Game over";
        }

        private Direction ReadMovement(Direction movement)
        {
            if (KeyAvailable)
            {
                var key = ReadKey(true).Key;

                if (key == ConsoleKey.UpArrow && movement != Direction.Down)
                {
                    movement = Direction.Up;
                }
                else if (key == ConsoleKey.DownArrow && movement != Direction.Up)
                {
                    movement = Direction.Down;
                }
                else if (key == ConsoleKey.LeftArrow && movement != Direction.Right)
                {
                    movement = Direction.Left;
                }
                else if (key == ConsoleKey.RightArrow && movement != Direction.Left)
                {
                    movement = Direction.Right;
                }
            }
            return movement;
        }
    }
}
