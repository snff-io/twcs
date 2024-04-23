using System;

// Static class to hold extension methods
public static class ArrayExtensions
{
    private static Random random = new Random();

    // Extension method for array to choose a random element
    public static T ChooseRandom<T>(this T[] array)
    {
        if (array == null || array.Length == 0)
        {
            throw new InvalidOperationException("Cannot choose an element from an empty array");
        }

        int index = random.Next(array.Length);
        return array[index];
    }
}
