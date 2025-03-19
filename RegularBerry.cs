using System;

namespace Snake
{
    public class RegularBerry : AbstractBerry
    {
        public override ConsoleColor Color => ConsoleColor.Cyan;
        
        public RegularBerry(int xPos, int yPos) : base(xPos, yPos)
        {
        }
        
        public override void OnEaten(Game game)
        {
            // Zvýšení skóre, přidání segmentu k hadovi
            game.IncreaseScore(1);
        }
    }
}