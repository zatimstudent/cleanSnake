using System.Diagnostics.CodeAnalysis;

namespace Snake;

public struct Pixel
{
    public Pixel (int xPos, int yPos, ConsoleColor color)
    {
        XPos = xPos;
        YPos = yPos;
        ScreenColor = color;
    }
    public int XPos { get; set; }
    public int YPos { get; set; }
    public ConsoleColor ScreenColor { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is Pixel other)
        {
            return XPos == other.XPos && YPos == other.YPos;
        }
        return false;
    }
}