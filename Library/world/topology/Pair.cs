using System.Diagnostics.CodeAnalysis;

namespace library.worldcomputer.info;

public partial class Pair
{

    public int X { get; set; }
    public int Y { get; set; }
    public int TopType { get; set; }
    public int BottomType { get; set; }
    public double Magnitude { get; set; } = 1;
    public double MaxMagnitude { get; set; } = 50;
    public double Pressure { get; set; } = 0;
    public int Layer { get; set; } = 0;
    public int Stability
    {
        get
        {
            if (this.Equal(Pair.None))
            {
                return 0;
            }
            return StabilityTable[BottomType][TopType];
        }
    }


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

    public Pair Copy()
    {
        return new Pair(X, Y, TopType, BottomType, Magnitude, Pressure, Layer);
    }

    public override string ToString()
    {
        return $"{Stability}{TopType}{BottomType};{Magnitude:F2};{Pressure:F2};{Layer}:{X}:{Y}";
    }

    public bool Equal(Pair other)
    {
        var areEqual = false;
        if (other != null)
        {
            areEqual = this.TopType == other.TopType && this.BottomType == other.BottomType;
        }

        return areEqual;
    }

    public int GetHashCode([DisallowNull] Pair obj)
    {
        throw new NotImplementedException();
    }
}
