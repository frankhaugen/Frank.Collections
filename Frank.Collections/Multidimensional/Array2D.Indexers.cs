namespace Frank.Collections.Multidimensional;

// File src/Frank.GameEngine.Primitives/SubPrimitives/Array2D.Indexers.cs
public partial class Array2D<T>
{
    /// <summary>
    /// Gets or sets the value at the specified position in the 2D array.
    /// </summary>
    /// <param name="position">The position in the 2D array.</param>
    /// <returns>The value at the specified position in the 2D array.</returns>
    public T? this[ArrayPosition2D position]
    {
        get => _array[position.Row, position.Column];
        set => _array[position.Row, position.Column] = value;
    }

    /// <summary>
    /// Gets or sets the value at the specified row and column index in the multidimensional array.
    /// </summary>
    /// <param name="row">The zero-based index of the row.</param>
    /// <param name="column">The zero-based index of the column.</param>
    /// <returns>The value at the specified position in the array.</returns>
    /// <remarks>
    /// If the specified position does not exist in the array, the getter will return null,
    /// and the setter will do nothing.
    /// </remarks>
    /// <typeparam name="T">The type of elements in the array.</typeparam>
    public T? this[uint row, uint column]
    {
        get => _array[row, column];
        set => _array[row, column] = value;
    }
}