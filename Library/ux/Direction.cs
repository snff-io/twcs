using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
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
                return Direction.up;
            case "d":
            case "down":
                return Direction.down;
            case "n":
            case "north":
                return Direction.north;
            case "s":
            case "south":
                return Direction.south;
            case "e":
            case "east":
                return Direction.east;
            case "w":
            case "west":
                return Direction.west;
            default:
                return Direction.none;
        }

    }
}

public enum Direction
{
    none,
    up,
    down,
    north,
    south,
    east,
    west
}