using YamlDotNet.Core;

public static class Directions
{
    public const string Up = "up";
    public const string Down = "down";
    public const string North = "north";
    public const string South = "south";
    public const string East = "east";
    public const string West = "west";

    public static Direction Parse(string dir)
    {
        switch (dir.ToLower())
        {
            case "u":
            case "up":
                return Direction.Up;
            case "d":
            case "down":
                return Direction.Down;
            case "n":
            case "north":
                return Direction.North;
            case "s":
            case "south":
                return Direction.South;
            case "e":
            case "east":
                return Direction.East;
            case "w":
            case "west":
                return Direction.West;
            default:
                throw new ArgumentException("Invalid direction");
        }

    }
}

public enum Direction
{
    Up,
    Down,
    North,
    South,
    East,
    West
}