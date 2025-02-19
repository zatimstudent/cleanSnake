namespace Snake;

public class GameBoard
{
    private readonly int _width;
    private readonly int _height;

    public GameBoard(int width, int height)
    {
        _width = width;
        _height = height;
    }

    public void DrawBorders()
    {
        for (int i = 0; i < _width; i++)
        {
            Console.SetCursorPosition(i, 0);
            Console.Write("■");
            Console.SetCursorPosition(i, _height - 1);
            Console.Write("■");
        }

        for (int i = 0; i < _height; i++)
        {
            Console.SetCursorPosition(0, i);
            Console.Write("■");
            Console.SetCursorPosition(_width - 1, i);
            Console.Write("■");
        }
    }

    public bool IsCollisionWithBorder(Position position)
    {
        return position.X == _width - 1 || position.X == 0 || 
               position.Y == _height - 1 || position.Y == 0;
    }
}