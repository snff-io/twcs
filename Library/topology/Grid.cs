namespace library.worldcomputer.info;



 class Grid {
    public Pair[][][] Layers { get; set; } = new Pair[0][][];

    public Grid(int layerSize = 379)
    {
        LayerSize = layerSize;
    }

    public int LayerSize { get; set; }
    public void InitializeGrid(int popLayers = 0)
    {
        var grid = new Pair[LayerSize][][];

        for (int layerIndex = 0; layerIndex < LayerSize; layerIndex++)
        {
            if (grid.Length >= LayerSize)
                break;

            if (grid.Length <= popLayers)
            {
                Pair[][] layer = new Pair[LayerSize][];
                for (int x = 0; x < LayerSize; x++)
                {
                    Pair[] col = new Pair[LayerSize];
                    layer[x] = col;

                    for (int y = 0; y < LayerSize; y++)
                    {
                        col[y] = Pair.RandomIndividual(x, y, grid.Length);
                    }
                }
                grid[grid.Length] = layer;
            }
            else
            {
                Pair[][] emptyLayer = new Pair[LayerSize][];
                for (int i = 0; i < LayerSize; i++)
                {
                    emptyLayer[i] = new Pair[LayerSize];
                }
                grid[grid.Length] = emptyLayer;
            }
        }
    }

    public IEnumerable<Pair[][]> GetPopulatedLayers()
    {
        var popLayers = new List<Pair[][]>();

        foreach (var layer in Layers)
        {
            if (layer.Any(pair => !pair.Equals(Pair.None)))
            {
                popLayers.Add(layer);
            }
        }

        return popLayers;
    }
}
