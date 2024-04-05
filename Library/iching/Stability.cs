public class Stability
{
    int[,] StabilityMatrix { get; set; }

    public Stability()
    {
        StabilityMatrix = new int[,]
        {
            //   0,   1 Heaven, 2 Earth, 3 Thunder, 4 Wind, 5 Water, 6 Fire, 7 Mountain, 8 Lake
            {   0,       1,       2,        3,        4,      5,       6,        7,         8 }, // 0 (Identifiers)
            {   1,       1,       1,        0,        0,      0,       1,        1,         0 }, // 1 Heaven
            {   2,       1,       1,        0,        0,      0,       1,        1,         0 }, // 2 Earth
            {   3,       0,       0,        1,        1,      1,       0,        0,         1 }, // 3 Thunder
            {   4,       0,       0,        1,        1,      0,       1,        1,         1 }, // 4 Wind
            {   5,       0,       0,        0,        0,      1,       1,        1,         1 }, // 5 Water
            {   6,       1,       1,        0,        0,      0,       1,        1,         0 }, // 6 Fire
            {   7,       1,       1,        0,        1,      1,       0,        1,         1 }, // 7 Mountain
            {   8,       0,       0,        1,        1,      1,       1,        0,         1 }, // 8 Lake
        };
    }

    public bool IsStable(int upper, int lower)
    {
        // Validate input
        if (upper < 1 || upper > 8 || lower < 1 || lower > 8)
        {
            throw new ArgumentOutOfRangeException("Trigram identifiers must be between 1 and 8.");
        }

        // Lookup stability in the matrix and return the corresponding boolean value
        return StabilityMatrix[upper, lower] == 1;
    }
}
