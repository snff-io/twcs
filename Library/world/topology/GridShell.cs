using library.worldcomputer.info;

public class GridShell : IGrid
{
    public int LayerSize { get;set; }
    public Pair[][][] Layers { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public void InitializeGrid(int layerSize = 190, int popLayers = 0)
    {
        LayerSize = layerSize;
    }
}