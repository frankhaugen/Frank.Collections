namespace Frank.Collections.Multidimensional;

// File Array2D.Find.cs
public partial class Array2D<T>
{
    /// <summary>
    /// Finds elements in the array that satisfy the given predicate.
    /// </summary>
    /// <param name="predicate">The function used to determine whether an element satisfies a condition.</param>
    /// <returns>An enumerable collection of elements that satisfy the given predicate.</returns>
    public IEnumerable<T?> Find(Func<T, bool> predicate) => _array.Find(predicate);

    /// <summary>
    /// Finds all elements that satisfy the given predicate in a specific row.
    /// </summary>
    /// <param name="row">The row index.</param>
    /// <param name="predicate">The predicate to test each element.</param>
    /// <returns>All elements in the specified row that satisfy the given predicate.</returns>
    public IEnumerable<T?> FindInRow(uint row, Func<T?, bool> predicate) => GetRow(row).Where(predicate);

    /// <summary>
    /// Finds and returns elements of type T in the specified column based on the given predicate.
    /// </summary>
    /// <typeparam name="T">The type of elements contained in the column.</typeparam>
    /// <param name="column">The column index to search in.</param>
    /// <param name="predicate">The condition to match elements in the column.</param>
    /// <returns>An IEnumerable containing elements of type T that satisfy the given predicate.</returns>
    public IEnumerable<T?> FindInColumn(uint column, Func<T?, bool> predicate) => GetColumn(column).Where(predicate);

    /// <summary>
    /// Finds elements in an array using a given predicate and positions.
    /// </summary>
    /// <param name="predicate">The predicate used to filter the elements.</param>
    /// <param name="positions">The positions to search in the array.</param>
    /// <returns>An enumerable sequence of elements matching the predicate at the specified positions.</returns>
    public IEnumerable<T?> FindIn(Func<T, bool> predicate, params ArrayPosition2D[] positions) => _array.FindIn(predicate, positions);
}