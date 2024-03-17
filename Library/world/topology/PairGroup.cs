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

    public PairGroup( Grid grid, Pair current)
    {
        var x = current.X;
        var y = current.Y;
        var layer = current.Layer;

        Current = current;
        North = y + 1 < grid.LayerSize ? grid.Layers[layer][x][y + 1] : Pair.None;
        South = y - 1 >= 0 ? grid.Layers[layer][x][y - 1] : Pair.None;
        East = x + 1 < grid.LayerSize ? grid.Layers[layer][x + 1][y] : Pair.None;
        West = x - 1 >= 0 ? grid.Layers[layer][x - 1][y] : Pair.None;
        Up = current.Layer + 1 < grid.LayerSize ? grid.Layers[layer + 1][x][y] : Pair.None;
        Down = current.Layer > 0 ? grid.Layers[layer - 1][x][y] : Pair.None;
    }

    public Pair Get(string direction) 
    {
        switch (direction) {
            case "North": 
                return North;
            case "South":
                return South;
            case "East":
                return East;
            case "West":
                return West;
            case "Up":
                return Up;
            case "Down":
                return Down;
            case "Current":
                return Current;
            default:
                throw new ArgumentOutOfRangeException();

        }
    }
}
