namespace library.worldcomputer.info;
using System.Collections.Generic;
public class Processor()
{

    public void SimulateSingleTurn(Grid grid)
    {
        for (int layerIndex = 0; layerIndex < grid.LayerSize; layerIndex++)
        {
            for (int x = 0; x < grid.LayerSize; x++)
            {
                for (int y = 0; y < grid.LayerSize; y++)
                {
                    Pair pair = grid.Layers[layerIndex][x][y];
                    if (pair != null && pair != Pair.None)
                    {
                        var pairGroup = new PairGroup(grid, pair);
                        UpdateMagnitude(pairGroup);
                        bool flip = UpdateStability(pairGroup);
                        FlipPair(grid, flip, pairGroup);
                    }
                }
            }
        }
    }
    public void UpdateMagnitude(PairGroup pairGroup)
    {
        pairGroup.Current.Magnitude += 0.1;
    }

    public bool UpdateStability(PairGroup pairGroup)
    {
        double stability = 0;
        double pressure = 0;

        if (pairGroup.Current.Stability == 1)
        {
            stability += pairGroup.Current.Magnitude;
        }
        else
        {
            pressure += (int)pairGroup.Current.Magnitude;
        }

        foreach (string direction in new[] { "north", "south", "east", "west" })
        {
            var neighbor = pairGroup.Get(direction);
            if (neighbor == null || neighbor == Pair.None)
            {
                pressure += 0.9;
            }
            else if (Pair.CheckStability(neighbor.TopType, pairGroup.Current.TopType) == 1)
            {
                stability += neighbor.Magnitude;
            }
            else
            {
                pressure += neighbor.Magnitude;
            }
        }

        if (pairGroup.Up != Pair.None)
        {
            if (Pair.CheckStability(pairGroup.Up.BottomType, pairGroup.Current.TopType) == 1)
            {
                stability += pairGroup.Up.Magnitude;
            }
            else
            {
                pressure += pairGroup.Up.Magnitude;
            }
        }

        if (pairGroup.Down != Pair.None)
        {
            if (Pair.CheckStability(pairGroup.Down.TopType, pairGroup.Current.BottomType) == 1)
            {
                stability += pairGroup.Down.Magnitude;
            }
            else
            {
                pressure += pairGroup.Down.Magnitude;
            }
        }

        return stability < pressure;
    }

    public void FlipPair(Grid grid, bool flip, PairGroup pairGroup)
    {
        if (flip)
        {
            if (pairGroup.Current.Stability == 1)
            {
                if (pairGroup.Current.Magnitude > pairGroup.Current.MaxMagnitude)
                {
                    if (pairGroup.Current.Layer + 1 < grid.LayerSize)
                    {
                        pairGroup.Up = pairGroup.Current.Copy();
                        pairGroup.Up.Layer = pairGroup.Current.Layer + 1;
                        pairGroup.Up.TopType = pairGroup.Current.BottomType;
                        pairGroup.Up.BottomType = pairGroup.Current.TopType;
                        grid.Layers[pairGroup.Up.Layer][pairGroup.Up.X][pairGroup.Up.Y] = pairGroup.Up;
                    }
                    pairGroup.Up.Magnitude = pairGroup.Current.Magnitude / 2;
                    pairGroup.Current.Magnitude = pairGroup.Current.Magnitude / 2;
                }
            }
            else
            {
                string[] directions = { "up", "down", "north", "south", "east", "west" };
                int neighbors = 0;
                foreach (string direction in directions)
                {
                    if (pairGroup.Get(direction) != Pair.None)
                    {
                        neighbors++;
                    }
                }
                foreach (string direction in directions)
                {
                    var neighbor = pairGroup.Get(direction);
                    if (neighbor != Pair.None)
                    {
                        neighbor.Magnitude += 0.1 / neighbors;
                    }
                }
                var temp = pairGroup.Current.TopType;
                pairGroup.Current.TopType = pairGroup.Current.BottomType;
                pairGroup.Current.BottomType = temp;
                pairGroup.Current.Magnitude -= 1;
                if (pairGroup.Current.Magnitude < 1 && pairGroup.Current.Layer == 0)
                {
                    pairGroup.Current.Magnitude = 1;
                }
                if (pairGroup.Current.Magnitude < 0)
                {
                    grid.Layers[pairGroup.Current.Layer][pairGroup.Current.X][pairGroup.Current.Y] = Pair.None;
                }
            }
        }
        else
        {
            if (pairGroup.Current.Stability == 1)
            {
                pairGroup.Current.Magnitude += 0.1;
            }
            else
            {
                string[] directions = { "up", "down", "north", "south", "east", "west" };
                int neighbors = 0;

                foreach (string direction in directions)
                {

                    if (pairGroup.Get(direction) != Pair.None)
                    {
                        neighbors++;
                    }
                }
                foreach (string direction in directions)
                {
                    var neighbor = pairGroup.Get(direction);
                    if (neighbor != Pair.None)
                    {
                        neighbor.Magnitude += 0.1 / neighbors;
                    }
                }
            }
        }
    }
}