using System.Formats.Tar;
using library.worldcomputer.info;

public interface IGrid
{
    public int LayerSize { get; set; }
    public Pair[][][] Layers { get; set; }
    void InitializeGrid(int layerSize = 190, int popLayers = 0);

    public Pair[][] this[int layer]
    {
        get
        {
            return Layers[layer];
        }
    }

    public PairGroup GetPairGroup(int layer, int x, int y)
    {
        return new PairGroup(this, layer, x, y);
    }

    public PairGroup GetRandomPoistion(int layer)
    {
        var tries = 20;
        do
        {
            var rx = Random.Shared.Next(1, LayerSize);
            var ry = Random.Shared.Next(1, LayerSize);
            var pg = GetPairGroup(layer, rx, ry);

            if (pg.Current != Pair.None)
                return pg;
            
            tries--;
        }
        while (tries > 0);
        
        throw new Exception("Failed to find non-empty position on layer " + layer.ToString());
    }
}