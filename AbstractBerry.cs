using System;

namespace Snake
{
    public abstract class AbstractBerry : IBerry
    {
        public int XPos { get; set; }
        public int YPos { get; set; }
        public abstract ConsoleColor Color { get; }

        protected AbstractBerry(int xPos, int yPos)
        {
            XPos = xPos;
            YPos = yPos;
        }

        public abstract void OnEaten(Game game);

        public Pixel GetPixel()
        {
            return new Pixel(XPos, YPos, Color);
        }
    }
}
