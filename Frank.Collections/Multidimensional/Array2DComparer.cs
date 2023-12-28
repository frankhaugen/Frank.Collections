using System.Diagnostics.CodeAnalysis;

namespace Frank.Collections.Multidimensional;

/// <summary>
/// Represents a comparer for comparing two-dimensional arrays.
/// </summary>
/// <typeparam name="T">The type of the elements in the arrays.</typeparam>
public class Array2DComparer<T> : IEqualityComparer<Array2D<T>>
{
    public bool Equals(Array2D<T>? x, Array2D<T>? y)
    {
        if (x == null || y == null)
            return false;

        if (x.Width != y.Width || x.Height != y.Height)
            return false;

        for (var i = 0u; i < x.Height; i++)
        for (var j = 0u; j < x.Width; j++)
            if (!x.Get(i, j)!.Equals(y.Get(i, j)))
                return false;

        return true;
    }

    public int GetHashCode([DisallowNull] Array2D<T> obj) => obj.GetHashCode();
}