using System;

namespace Snake
{
    public interface IBerry
    {
        int XPos { get; set; }
        int YPos { get; set; }
        ConsoleColor Color { get; }
        
        // když had sní bobuli
        void OnEaten(Game game);
        
        // pro vykreslení bobule
        Pixel GetPixel();
    }
}