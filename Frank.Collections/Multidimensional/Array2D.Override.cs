using Frank.Collections.Extensions;

namespace Frank.Collections.Multidimensional;

// File: Array2D.Override.cs
public partial class Array2D<T>
{
    /// <summary>
    /// Converts the current instance to its equivalent string representation.
    /// </summary>
    /// <returns>
    /// A string representation of the current instance.
    /// </returns>
    public override string ToString() => _array.Select2D(x => x?.ToString() ?? "").AsString();

    /// <summary>
    /// Computes the hash code for the current object.
    /// </summary>
    /// <remarks>
    /// This uses the <see cref="HashCode"/> class to compute the hash code and iterates over all the elements in both dimensions of the array to compute the hash code. This is a very expensive operation and should be used with caution if the array is large and/or the operation is performed frequently.
    /// </remarks>
    /// <returns>
    /// A hash code for the current object.
    /// </returns>
    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        for (var i = 0u; i < Width; i++)
        for (var j = 0u; j < Height; j++)
            hashCode.Add(_array[i, j]);
        return hashCode.ToHashCode();
    }

    /// <summary>
    /// Determines whether the current object is equal to another object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object. </param>
    /// <returns>
    /// <c>true</c> if the current object is equal to the <paramref name="obj"/> parameter; otherwise, <c>false</c>.
    /// </returns>
    public override bool Equals(object? obj)
    {
        if (obj is not Array2D<T> array2D) return false;
        if (array2D.Width != Width || array2D.Height != Height) return false;
        return GetHashCode() == array2D.GetHashCode();
    }
}