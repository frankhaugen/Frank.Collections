namespace Frank.Collections.Multidimensional;

// File: Array2D.Get.cs
public partial class Array2D<T>
{
    /// <summary>
    /// Retrieves the value at the specified position in the array.
    /// </summary>
    /// <param name="position">The position of the element to retrieve.</param>
    /// <returns>The value at the specified position, or null if the position is out of range.</returns>
    public T? Get(ArrayPosition2D position) => _array[position.Row, position.Column];

    /// <summary>
    /// Gets the value at the specified position in the array.
    /// </summary>
    /// <param name="x">The x-coordinate of the position.</param>
    /// <param name="y">The y-coordinate of the position.</param>
    /// <returns>The value at the specified position, or null if the position is out of range.</returns>
    public T? Get(uint x, uint y) => _array[y, x];

    /// <summary>
    /// Retrieves a row from the two-dimensional array.
    /// </summary>
    /// <param name="row">The row index to retrieve.</param>
    /// <returns>An enumerable collection of nullable values representing the elements in the specified row.</returns>
    public IEnumerable<T?> GetRow(uint row)
    {
        var rowArray = new T?[Width];
        for (var i = 0u; i < Width; i++) rowArray[i] = _array[row, i];
        return rowArray;
    }

    /// <summary>
    /// Retrieves a specific column from a 2D array.
    /// </summary>
    /// <typeparam name="T">The type of elements contained in the array.</typeparam>
    /// <param name="column">The index of the column to retrieve.</param>
    /// <returns>An enumerable containing the elements of the specified column.</returns>
    public IEnumerable<T?> GetColumn(uint column)
    {
        var columnArray = new T?[Height];
        for (var i = 0u; i < Height; i++) columnArray[i] = _array[i, column];
        return columnArray;
    }
}