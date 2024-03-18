namespace library.worldcomputer.info;

public partial class Pair
{

    static Random RAND = new Random(144000);
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
        int topType = RAND.Next(1, 9);
        int bottomType = RAND.Next(1, 9);
        int magnitude = 1;
        int pressure = 0;

        return new Pair(x, y, topType, bottomType, magnitude, pressure, layer);
    }

    static Pair none = new Pair(-1,-1,-1,-1,-1,-1,-1);
    public static Pair None => none;
}