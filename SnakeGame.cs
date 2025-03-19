namespace Snake;

public class SnakeGame
{
    private readonly GameBoard _board;
    private readonly SnakeHead _head;
    private readonly List<int> _bodyXPositions;
    private readonly List<int> _bodyYPositions;
    private readonly List<Berry> _berries;
    private Direction _currentDirection;
    private GameState _gameState;
    private int _score;
    private readonly Random _random;

    public int Score
    {
        get { return _score; }
    }
    
    public void UpdateScore(int points)
    {
        _score += points;
        
        if (_score < 0)
        {
            _score = 0;
        }
    }
    
    public SnakeGame(int width, int height)
    {
        _board = new GameBoard(width, height);
        _head = new SnakeHead
        {
            X = width / 2,
            Y = height / 2,
            Color = ConsoleColor.Red
        };
        _bodyXPositions = new List<int>();
        _bodyYPositions = new List<int>();
        _random = new Random();
        _berries = new List<Berry>
        {
            GenerateNewBerry(BerryType.Regular),
            GenerateNewBerry(BerryType.Poison)
        };
        _currentDirection = Direction.RIGHT;
        _gameState = GameState.Running;
        _score = 5;
    }

    private Position GenerateNewBerryPosition()
    {
        return new Position
        {
            X = _random.Next(1, Console.WindowWidth - 1),
            Y = _random.Next(1, Console.WindowHeight - 1)
        };
    }

    private Berry GenerateNewBerry(BerryType berryType)
    {
        Position newPosition = GenerateNewBerryPosition();

        return berryType switch
        {
            BerryType.Poison => new PoisonBerry(newPosition),
            BerryType.Regular => new RegularBerry(newPosition),
            _ => throw new ArgumentOutOfRangeException(nameof(berryType), berryType, null)
        };
    }
    
    public void Update()
    {
        Console.Clear();
        _board.DrawBorders();

        if (_board.IsCollisionWithBorder(_head))
        {
            EndGame("Snake hit the border.");
            return;
        }

        DrawBerry();
        CheckBerryCollision();
        UpdateSnakeBody();
        HandleMovement();
    }

    private void DrawBerry()
    {
        foreach (var berry in _berries)
        {
            berry.Draw();
        }
    }

    private void CheckBerryCollision()
    {
        for (int i = 0; i < _berries.Count; i++)
        {
            if (_berries[i].IsCollision(_head))
            {
                _berries[i].ApplyEffect(this);
                _berries.RemoveAt(i);
                _berries.Add(GenerateNewBerry(BerryType.Poison));
                _berries.Add(GenerateNewBerry(BerryType.Regular));
                break;
            }
        }
    }

    private void UpdateSnakeBody()
    {
        for (int i = 0; i < _bodyXPositions.Count; i++)
        {
            if (_bodyXPositions[i] == _head.X && _bodyYPositions[i] == _head.Y)
            {
                EndGame("Snake collided with itself.");
                return;
            }
        }
        
        _bodyXPositions.Add(_head.X);
        _bodyYPositions.Add(_head.Y);

        if (_bodyXPositions.Count > _score)
        {
            _bodyXPositions.RemoveAt(0);
            _bodyYPositions.RemoveAt(0);
        }

        for (int i = 0; i < _bodyXPositions.Count; i++)
        {
            Console.SetCursorPosition(_bodyXPositions[i], _bodyYPositions[i]);
            Console.Write("■");
        }
    }

    private void HandleMovement()
    {
        if (Console.KeyAvailable)
        {
            var key = Console.ReadKey(true);
            UpdateDirection(key.Key);
        }
        MoveHead();
    }

    private void UpdateDirection(ConsoleKey key)
    {
        switch (key)
        {
            case ConsoleKey.UpArrow when _currentDirection != Direction.DOWN:
                _currentDirection = Direction.UP;
                break;
            case ConsoleKey.DownArrow when _currentDirection != Direction.UP:
                _currentDirection = Direction.DOWN;
                break;
            case ConsoleKey.LeftArrow when _currentDirection != Direction.RIGHT:
                _currentDirection = Direction.LEFT;
                break;
            case ConsoleKey.RightArrow when _currentDirection != Direction.LEFT:
                _currentDirection = Direction.RIGHT;
                break;
        }
    }

    private void MoveHead()
    {
        switch (_currentDirection)
        {
            case Direction.UP:
                _head.Y--;
                break;
            case Direction.DOWN:
                _head.Y++;
                break;
            case Direction.LEFT:
                _head.X--;
                break;
            case Direction.RIGHT:
                _head.X++;
                break;
        }
    }

    private void EndGame(string message)
    {
        _gameState = GameState.Over;
        Console.SetCursorPosition(Console.WindowWidth / 5, Console.WindowHeight / 2);
        Console.WriteLine($"Game Over: {message} Score: {_score}");
    }

    public bool IsRunning => _gameState == GameState.Running;
}