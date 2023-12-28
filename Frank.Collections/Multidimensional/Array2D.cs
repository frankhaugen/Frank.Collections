using Frank.Collections.Extensions;

namespace Frank.Collections.Multidimensional;

/// <summary>
/// Represents a two-dimensional array of type T.
/// </summary>
/// <typeparam name="T">The type of elements in the array.</typeparam>
public partial class Array2D<T>
{
    private readonly T?[,] _array;

    public uint Width => (uint) _array.GetLength(0);
    public uint Height => (uint) _array.GetLength(1);

    /// <summary>
    /// Initializes a new instance of the Array2D class with the specified width and height.
    /// </summary>
    /// <param name="width">The width of the 2D array.</param>
    /// <param name="height">The height of the 2D array.</param>
    public Array2D(uint width, uint height) => _array = new T?[width, height];

    /// <summary>
    /// Initializes a new instance of the Array2D class with the specified 2D array.
    /// </summary>
    /// <param name="array">The 2D array to set as the underlying data storage for the Array2D object.</param>
    public Array2D(T?[,] array) => _array = array;

    /// <summary>
    ///    Returns a copy of the internal array.
    /// </summary>
    /// <returns></returns>
    public T?[,] ToArray()
    {
        var newArray = new T?[,] {};
        for (var i = 0u; i < Width; i++)
        {
            for (var j = 0u; j < Height; j++)
            {
                newArray[i, j] = _array[i, j];
            }
        }
        
        return newArray;
    }

    /// <summary>
    /// Returns the internal array as a reference.
    /// </summary>
    /// <remarks>
    ///    This method is unsafe and should be used with caution. Use <see cref="ToArray"/> instead so you get a copy of the internal array instead of a reference.
    /// </remarks>
    /// <returns></returns>
    public T?[,] ToUnsafeArray() => _array;

    /// <summary>
    /// Converts the current instance to its equivalent string representation.
    /// </summary>
    /// <returns>
    /// A string representation of the current instance.
    /// </returns>
    public override string ToString() => _array.Select2D(x => x?.ToString() ?? "").AsString();
}