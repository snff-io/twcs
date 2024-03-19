using Microsoft.AspNetCore.DataProtection;
using MongoDB.Bson.Serialization.Serializers;
using System.Linq;
namespace library.worldcomputer.info;



public class Grid
{
    public Pair[][][] Layers { get; set; } = new Pair[0][][];


    public DateTime Initialized = DateTime.MaxValue;

    public Grid(int layerSize = 37)
    {
        LayerSize = layerSize;
    }

    public int LayerSize { get; set; }
    public void InitializeGrid(int popLayers = 0)
    {
        var grid = new Pair[LayerSize][][];

        for (int layerIndex = 0; layerIndex < LayerSize; layerIndex++)
        {
            if (layerIndex <= popLayers)
            {
                Pair[][] layer = new Pair[LayerSize][];
                for (int x = 0; x < LayerSize; x++)
                {
                    Pair[] col = new Pair[LayerSize];
                    layer[x] = col;

                    for (int y = 0; y < LayerSize; y++)
                    {
                        col[y] = Pair.RandomIndividual(x, y, layerIndex);
                    }
                }
                grid[layerIndex] = layer;
            }
            else
            {
                Pair[][] emptyLayer = new Pair[LayerSize][];
                for (int i = 0; i < LayerSize; i++)
                {
                    emptyLayer[i] = new Pair[LayerSize];
                    for (int j = 0; j < LayerSize; j++)
                    {
                        var ep = Pair.None.Copy();
                        ep.Layer = layerIndex;
                        ep.X = i;
                        ep.Y = j;
                        emptyLayer[i][j] = ep;
                    }
                }
                grid[layerIndex] = emptyLayer;
            }
        }
        Layers = grid;
        Initialized = DateTime.Now;
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

    // public IEnumerable<Pair> GetAllPairs()
    // {
    //     var ret = new List<Pair>();

    //     for (int l = 0; l < this.Layers.Count(); l++)
    //     {
    //         for (int x = 0; x < this.Layers[l].Count(); x++)
    //         {
    //             for (int y = 0; y < this.Layers[l][x].Count(); y++)
    //             {
    //                 ret.Add(this.Layers[l][x][y]);
    //             }
    //         }
    //     }

    //     return ret;
    // }

    public IEnumerable<Pair> GetAllPairs() {
        return Layers.SelectMany(layer => layer.SelectMany(row => row));
    }
}
