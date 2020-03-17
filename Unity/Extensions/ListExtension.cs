using System.Collections;
using System.Collections.Generic;

public static class ListExtension
{
    /// <summary> Return true if index is within the list range </summary>
    public static bool InRange(this IList self, int index)
    {
        return index > 0 && index < self.Count;
    }

    /// <summary> Get random element in a list </summary>
    public static T Random<T>(this IList<T> self)
    {
        return (T)self[UnityEngine.Random.Range(0, self.Count)];
    }

    /// <summary> Get random element in a list </summary>
    public static T Random<T>(this IList<T> self, ref System.Random seededRandom)
    {
        return (T)self[seededRandom.Next(0, self.Count)];
    }
}
