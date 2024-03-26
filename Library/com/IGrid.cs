using library.worldcomputer.info;

public interface IGrid 
{
    public int LayerSize {get;set;}
    public Pair[][][] Layers {get;set;}
    void InitializeGrid(int layerSize = 190,  int popLayers = 0);

    public Pair[][] this[int layer] {
        get
        {
            return Layers[layer];
        }
    }
}