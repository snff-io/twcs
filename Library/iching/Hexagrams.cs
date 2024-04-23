public static class Hexagrams
{
    static Dictionary<string, string> _descriptions;


    public class IntArrayEqualityComparer : IEqualityComparer<int[]>
    {
        public bool Equals(int[] x, int[] y)
        {
            if (x == null || y == null)
                return false;

            if (x.Length != y.Length)
                return false;

            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] != y[i])
                    return false;
            }

            return true;
        }

        public int GetHashCode(int[] obj)
        {
            unchecked
            {
                int hash = 17;
                foreach (var item in obj)
                {
                    hash = hash * 23 + item.GetHashCode();
                }
                return hash;
            }
        }
    }
}