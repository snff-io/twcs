namespace library.worldcomputer.info;

public class Pair
{
    public static int[][] StabilityTable { get; private set; } = new int[][]
    {
        new int[] { -1, 1, 2, 3, 4, 5, 6, 7, 8 },
        new int[] { 1, 1, 0, 0, 0, 0, 0, 0, 0 },
        new int[] { 2, 1, 1, 1, 0, 1, 1, 1, 1 },
        new int[] { 3, 1, 0, 1, 1, 1, 1, 0, 0 },
        new int[] { 4, 1, 0, 0, 1, 1, 1, 0, 0 },
        new int[] { 5, 1, 0, 0, 0, 1, 0, 0, 0 },
        new int[] { 6, 1, 0, 0, 0, 1, 1, 0, 0 },
        new int[] { 7, 1, 0, 0, 0, 1, 1, 1, 1 },
        new int[] { 8, 1, 0, 0, 1, 1, 1, 0, 1 }
    };
    public static int CheckStability(int topType, int bottomType)
    {
        return StabilityTable[bottomType][topType];
    }

    public static Pair RandomIndividual(int x, int y, int layer = 0)
    {
        int topType = new Random().Next(1, 9);
        int bottomType = new Random().Next(1, 9);
        int magnitude = 1;
        int pressure = 0;

        return new Pair(x, y, topType, bottomType, magnitude, pressure, layer);
    }

    public int X { get; }
    public int Y { get; }
    public int TopType { get; }
    public int BottomType { get; }
    public int Magnitude { get; private set; } = 1;
    public int Pressure { get; private set; } = 0;
    public int Layer { get; private set; } = 0;

    public Pair(int x, int y, int topType, int bottomType, int magnitude = 1, int pressure = 0, int layer = 0)
    {
        X = x;
        Y = y;
        TopType = topType;
        BottomType = bottomType;
        Magnitude = magnitude;
        Pressure = pressure;
        Layer = layer;
    }

    public int Stability => StabilityTable[BottomType][TopType];

    public Pair Copy()
    {
        return new Pair(X, Y, TopType, BottomType, Magnitude, Pressure, Layer);
    }

    public override string ToString()
    {
        return $"{Stability}{TopType}{BottomType};{Magnitude:F2};{Pressure:F2};{Layer}:{X}:{Y}";
    }


}
