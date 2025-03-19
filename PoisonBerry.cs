using System;

namespace Snake
{
    public class PoisonBerry : AbstractBerry
    {
        public override ConsoleColor Color => ConsoleColor.Magenta;
        
        public PoisonBerry(int xPos, int yPos) : base(xPos, yPos)
        {
        }
        
        public override void OnEaten(Game game)
        {
            game.DecreaseScore(2);
            // game.EndGame();
        }
    }
}