namespace library.worldcomputer.info;
public class PairGroup
{

    public Pair Current { get; set; }
    public Pair North { get; set; }
    public Pair South { get; set; }
    public Pair East { get; set; }
    public Pair West { get; set; }
    public Pair Up { get; set; }
    public Pair Down { get; set; }


    public PairGroup(Grid grid, int layer, int x, int y)
    {
       try
        {
            Current = grid.Layers[layer][x][y];
            North = y + 1 < grid.LayerSize - 1 ? grid.Layers[layer][x][y + 1] : Pair.None;
            South = y - 1 >= 0 ? grid.Layers[layer][x][y - 1] : Pair.None;
            East = x + 1 < grid.LayerSize - 1 ? grid.Layers[layer][x + 1][y] : Pair.None;
            West = x - 1 >= 0 ? grid.Layers[layer][x - 1][y] : Pair.None;
            Up = layer + 1 < grid.LayerSize - 1 ? grid.Layers[layer + 1][x][y] : Pair.None;
            Down = layer > 0 ? grid.Layers[layer - 1][x][y] : Pair.None;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public PairGroup(Grid grid, Pair current)
    :this(grid, current.Layer, current.X, current.Y)
    {
 
    }


    public Pair Get(string direction)
    {
        switch (direction)
        {
            case "North":
            case "north":
                return North;
            case "South":
            case "south":
                return South;
            case "East":
            case "east":
                return East;
            case "West":
            case "west":
                return West;
            case "Up":
            case "up":
                return Up;
            case "Down":
            case "down":
                return Down;
            case "Current":
            case "current":
                return Current;
            default:
                throw new ArgumentOutOfRangeException();

        }
    }
}
