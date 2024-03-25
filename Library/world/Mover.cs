using System.ComponentModel;
using library.worldcomputer.info;
using Microsoft.AspNetCore.Components.Web;

public class Mover : IMove
{
    private IGrid _grid;
    private IWordResolver _wordResolver;

    public Mover(IGrid grid)
    {
        _grid = grid;

    }

    public Mover(IWordResolver wordResolver)
    {
        _wordResolver = wordResolver;
    }

    public bool TryMove(ILocation location, Direction direction)
    {
        switch (direction)
        {
            case Direction.up:
                if (location.Layer + 1 > _grid.LayerSize)
                {
                    location.Layer += 1;
                    return true;
                }
                else
                {
                    return false;
                }

            case Direction.down:
                if (location.Layer - 1 >= 0)
                {
                    location.Layer -= 1;
                    return true;
                }
                else
                {
                    return false;
                }

            case Direction.north:
                if (location.Y - 1 >= 0)
                {
                    location.Y -= 1;
                    return true;
                }
                else
                {
                    return false;
                }

            case Direction.south:
                if (location.Y + 1 < _grid.LayerSize)
                {
                    location.Y += 1;
                    return true;
                }
                else
                {
                    return false;
                }

            case Direction.west:
                if (location.X - 1 >= 0)
                {
                    location.X -= 1;
                    return true;
                }
                else
                {
                    return false;
                }

            case Direction.east:
                if (location.X + 1 < _grid.LayerSize)
                {
                    location.X += 1;
                    return true;
                }
                else
                {
                    return false;
                }

            default:
                throw new ArgumentException("Invalid direction");
        }
    }

    public bool TryParse(string arguments, out string intentPath)
    {
    
        intentPath = "";
        var travel = _wordResolver.Resolve(arguments, PartOfSpeech.verb, "travel" );

        if (travel != "")
        {
            intentPath = "travel";

            //extract Direction
            var direction = _wordResolver.Resolve(arguments, PartOfSpeech.noun, "north","south", "east","west","up","down");
            
            if (direction != "")
                intentPath += "." + direction;

            
            //TODO:
            //extract optional number from arguments
        }

        return intentPath != ""; 
        
    }
}