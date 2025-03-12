namespace Snake;

public class RegularBerry : Berry
{
    public RegularBerry(Position position) : base(position)
    {
        Color = ConsoleColor.Cyan;
    }
    
    public override void ApplyEffect(SnakeGame game)
    {
        game.UpdateScore(1);
    }
}