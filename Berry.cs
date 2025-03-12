namespace Snake;

public abstract class Berry
{
    public Position Position { get; protected set; }
    public ConsoleColor Color { get; protected set; }
    public char Symbol { get; protected set; } = '■';
    
    public Berry(Position position)
    {
        Position = position;
    }
    
    public virtual void Draw()
    {
        Console.SetCursorPosition(Position.X, Position.Y);
        Console.ForegroundColor = Color;
        Console.Write(Symbol);
    }
    
    public abstract void ApplyEffect(SnakeGame game);
    
    public bool IsCollision(Position snakeHeadPosition)
    {
        return Position.X == snakeHeadPosition.X && Position.Y == snakeHeadPosition.Y;
    }
}