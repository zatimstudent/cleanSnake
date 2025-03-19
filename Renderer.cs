
using static System.Console;

namespace Snake
{
    public class Renderer
    {
        public static void DrawPixel(Pixel pixel)
        {
            SetCursorPosition(pixel.XPos, pixel.YPos);
            ForegroundColor = pixel.ScreenColor;
            Write("■");
            SetCursorPosition(0, 0);
        }

        public static void DrawBorder(int width, int height)
        {
            for (int i = 0; i < width; i++)
            {
                SetCursorPosition(i, 0);
                Write("■");

                SetCursorPosition(i, height - 1);
                Write("■");
            }

            for (int i = 0; i < height; i++)
            {
                SetCursorPosition(0, i);
                Write("■");

                SetCursorPosition(width - 1, i);
                Write("■");
            }
        }
        
        public static void ShowGameOver(int score, int width, int height)
        {
            SetCursorPosition(width / 5, height / 2);
            WriteLine($"Game over, Score: {score - 5}");
            SetCursorPosition(width / 5, height / 2 + 1);
        }
    }
}