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

    public double MaxMagnitude = 50;

    static Pair none =  new Pair(0,0,0,0,0,0,0);
    public static Pair None =>none;

    public int X { get; set; }
    public int Y { get; set; }
    public int TopType { get; set; }
    public int BottomType { get; set; }
    public double Magnitude { get; set; } = 1;
    public double Pressure { get; set; } = 0;
    public int Layer { get; set; } = 0;

    public Pair(int x, int y, int topType, int bottomType, double magnitude = 1, double pressure = 0, int layer = 0)
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
