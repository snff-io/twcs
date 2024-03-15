using System;

public class MyClass
{
    public int GridSize { get; private set; }
    public int NumLayers { get; private set; }
    public int MaxMagnitude { get; private set; }
    public object Font { get; private set; }
    public Pair[][][] Grid { get; private set; }
    public bool WEB_GL { get; private set; } = true;
    public const int Vp = 1024;

  


    public void InitializeGrid(int size, int layerSize, int popLayers = 0)
    {
        Grid = new Pair[size][][];

        for (int layerIndex = 0; layerIndex < layerSize; layerIndex++)
        {
            if (Grid.Length >= size)
                break;

            if (Grid.Length <= popLayers)
            {
                Pair[][] layer = new Pair[size][];
                for (int x = 0; x < size; x++)
                {
                    Pair[] col = new Pair[size];
                    layer[x] = col;

                    for (int y = 0; y < size; y++)
                    {
                        col[y] = RandomIndividual(x, y, Grid.Length);
                    }
                }
                Grid[Grid.Length] = layer;
            }
            else
            {
                Pair?[][] emptyLayer = new Pair?[size][];
                for (int i = 0; i < size; i++)
                {
                    emptyLayer[i] = new Pair?[size];
                }
                Grid[Grid.Length] = emptyLayer;
            }
        }
    }

    public void SimulateSingleTurn()
    {
        for (int layerIndex = 0; layerIndex < NumLayers; layerIndex++)
        {
            for (int x = 0; x < GridSize; x++)
            {
                for (int y = 0; y < GridSize; y++)
                {
                    Pair? pair = Grid[layerIndex][x][y];
                    if (pair != null)
                    {
                        var pairGroup = GetPairGroup(pair, x, y);
                        UpdateMagnitude(pairGroup);
                        var flip = UpdateStability(pairGroup);
                        FlipPair(flip, pairGroup);
                    }
                }
            }
        }
    }

    public Pair?[] GetPairGroup(Pair pair, int x, int y)
    {
        Pair?[] pairGroup = new Pair?[6];
        pairGroup[0] = pair;
        pairGroup[1] = y + 1 < GridSize ? Grid[pair.Layer][x][y + 1] : null;
        pairGroup[2] = y - 1 >= 0 ? Grid[pair.Layer][x][y - 1] : null;
        pairGroup[3] = x + 1 < GridSize ? Grid[pair.Layer][x + 1][y] : null;
        pairGroup[4] = x - 1 >= 0 ? Grid[pair.Layer][x - 1][y] : null;
        pairGroup[5] = pair.Layer + 1 < NumLayers ? Grid[pair.Layer + 1][x][y] : null;
        return pairGroup;
    }

    public void UpdateMagnitude(Pair?[] pairGroup)
    {
        pairGroup[0]?.Magnitude += 1;
    }

    public bool UpdateStability(Pair?[] pairGroup)
    {
        int stability = 0;
        int pressure = 0;

        if (pairGroup[0]?.Stability == 1)
        {
            stability += pairGroup[0].Magnitude;
        }
        else
        {
            pressure += pairGroup[0].Magnitude;
        }

        for (int i = 1; i < 6; i++)
        {
            if (pairGroup[i] == null)
            {
                pressure += 9;
            }
            else if (CheckStability(pairGroup[i].TopType, pairGroup[0].TopType) == 1)
            {
                stability += pairGroup[i].Magnitude;
            }
            else
            {
                pressure += pairGroup[i].Magnitude;
            }
        }

        if (pairGroup[5] != null)
        {
            if (CheckStability(pairGroup[5].BottomType, pairGroup[0].TopType) == 1)
            {
                stability += pairGroup[5].Magnitude;
            }
            else
            {
                pressure += pairGroup[5].Magnitude;
            }
        }

        return stability < pressure;
    }

    public void FlipPair(bool flip, Pair?[] pairGroup)
    {
        if (flip)
        {
            if (pairGroup[0]?.Stability == 1)
            {
                if (pairGroup[0].Magnitude > MaxMagnitude)
                {
                    if (pairGroup[0].Layer + 1 < NumLayers && pairGroup[5] == null)
                    {
                        pairGroup[5] = pairGroup[0].Copy();
                        pairGroup[5].Layer = pairGroup[0].Layer + 1;
                        pairGroup[5].TopType = pairGroup[0].BottomType;
                        pairGroup[5].BottomType = pairGroup[0].TopType;
                        Grid[pairGroup[5].Layer][pairGroup[5].X][pairGroup[5].Y] = pairGroup[5];
                    }
                    pairGroup[5].Magnitude = pairGroup[0].Magnitude / 2;
                    pairGroup[0].Magnitude = pairGroup[0].Magnitude / 2;
                }
            }
            else
            {
                int neighbors = 0;
                foreach (var neighbor in pairGroup)
                {
                    if (neighbor != null)
                    {
                        neighbors++;
                    }
                }
                foreach (var neighbor in pairGroup)
                {
                    if (neighbor != null)
                    {
                        neighbor.Magnitude += 1 / neighbors;
                    }
                }
                int temp = pairGroup[0].TopType;
                pairGroup[0].TopType = pairGroup[0].BottomType;
                pairGroup[0].BottomType = temp;
                pairGroup[0].Magnitude -= 1;
                if (pairGroup[0].Magnitude < 1 && pairGroup[0].Layer == 0)
                {
                    pairGroup[0].Magnitude = 1;
                }
                if (pairGroup[0].Magnitude < 0)
                {
                    Grid[pairGroup[0].Layer][pairGroup[0].X][pairGroup[0].Y] = null;
                }
            }
        }
        else
        {
            if (pairGroup[0]?.Stability == 1)
            {
                pairGroup[0].Magnitude += 0.1;
            }
            else
            {
                int neighbors = 0;
                foreach (var neighbor in pairGroup)
                {
                    if (neighbor != null)
                    {
                        neighbors++;
                    }
                }
                foreach (var neighbor in pairGroup)
                {
                    if (neighbor != null)
                    {
                        neighbor.Magnitude += 0.1 / neighbors;
                    }
                }
            }
        }
    }
}
