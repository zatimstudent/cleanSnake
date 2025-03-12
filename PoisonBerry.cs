namespace Snake;

public class PoisonBerry : Berry
{
    public PoisonBerry(Position position) : base(position)
    {
        Color = ConsoleColor.Green;
        Symbol = '♦';
    }
    
    public override void ApplyEffect(SnakeGame game)
    {
        game.UpdateScore(-2);
    }
}