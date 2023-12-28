namespace Frank.Collections.Multidimensional;

public partial class Array2D<T>
{
    /// <summary>
    /// Creates a new Array2D<T> object as a slice of the current Array2D<T> object.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the array.</typeparam>
    /// <param name="x">The starting x-coordinate of the slice.</param>
    /// <param name="y">The starting y-coordinate of the slice.</param>
    /// <param name="width">The width of the slice.</param>
    /// <param name="height">The height of the slice.</param>
    /// <returns>A new Array2D<T> object representing the slice of the original array.</returns>
    public Array2D<T> Slice(uint x, uint y, uint width, uint height)
    {
        var slice = new Array2D<T>(width, height);
        for (var i = x; i < x + width; i++)
        for (var j = y; j < y + height; j++)
            slice[i - x, j - y] = _array[i, j];
        return slice;
    }
}