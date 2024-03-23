using library.worldcomputer.info;

public class Mover : IMove
{
    private IGrid _grid;

    public Mover(IGrid grid)
    {
        _grid = grid;

    }

public bool TryMove(ILocation location, Direction direction)
{
    switch (direction)
    {
        case Direction.Up:
            if (location.Layer + 1 > _grid.LayerSize)
            {
                location.Layer += 1;
                return true;
            }
            else
            {
                return false;
            }

        case Direction.Down:
            if (location.Layer - 1 >= 0)
            {
                location.Layer -= 1;
                return true;
            }
            else
            {
                return false;
            }

        case Direction.North:
            if (location.Y - 1 >= 0)
            {
                location.Y -= 1;
                return true;
            }
            else
            {
                return false;
            }

        case Direction.South:
            if (location.Y + 1 < _grid.LayerSize)
            {
                location.Y += 1;
                return true;
            }
            else
            {
                return false;
            }

        case Direction.West:
            if (location.X - 1 >= 0)
            {
                location.X -= 1;
                return true;
            }
            else
            {
                return false;
            }

        case Direction.East:
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



}