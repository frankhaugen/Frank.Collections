using System.Diagnostics.CodeAnalysis;

namespace Frank.Collections.Multidimensional;

public static class Array2DExtensions
{
    /// <summary>
    /// Retrieves the value at the specified position in a two-dimensional nullable array.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the array.</typeparam>
    /// <param name="array">The two-dimensional nullable array to retrieve value from.</param>
    /// <param name="position">The position in the array.</param>
    /// <returns>The value at the specified position in the array, or null if the position is out of range.</returns>
    public static T? GetValue<T>(this T?[,] array, ArrayPosition2D position) => array[position.Row, position.Column];

    /// <summary>
    /// Gets the value of a nullable element in a two-dimensional array at the specified coordinates.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the array.</typeparam>
    /// <param name="array">The two-dimensional array.</param>
    /// <param name="x">The horizontal coordinate.</param>
    /// <param name="y">The vertical coordinate.</param>
    /// <returns>The value of the nullable element at the specified coordinates.</returns>
    public static T? GetValue<T>(this T?[,] array, uint x, uint y) => array[y, x];

    /// <summary>
    /// Retrieves all the values from a given 2-dimensional array.
    /// </summary>
    /// <typeparam name="T">The type of the array elements.</typeparam>
    /// <param name="array">The 2-dimensional array.</param>
    /// <returns>An enumerable containing all the values in the array.</returns>
    public static IEnumerable<T?> GetValues<T>(this T?[,] array)
    {
        for (var i = 0u; i < array.GetLength(0); i++)
        for (var j = 0u; j < array.GetLength(1); j++)
            yield return array[i, j];
    }

    /// <summary>
    /// Attempts to retrieve the value at the specified position in a nullable two-dimensional array.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the array.</typeparam>
    /// <param name="array">The nullable two-dimensional array.</param>
    /// <param name="position">The position at which to retrieve the value.</param>
    /// <param name="value">When this method returns, contains the value at the specified position, if found; otherwise, the default value of the type.</param>
    /// <returns>
    /// <c>true</c> if the value at the specified position is not null; otherwise, <c>false</c>.
    /// </returns>
    public static bool TryGetValue<T>(this T?[,] array, ArrayPosition2D position, out T? value)
    {
        value = array[position.Row, position.Column];
        return value != null;
    }

    /// <summary>
    /// Tries to get the value at the specified index in a 2D nullable array.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the array.</typeparam>
    /// <param name="array">The 2D nullable array.</param>
    /// <param name="x">The x-axis index.</param>
    /// <param name="y">The y-axis index.</param>
    /// <param name="value">When this method returns, contains the value at the specified index if it exists, otherwise, the default value of T?. This parameter is passed uninitialized.</param>
    /// <returns>true if a value exists at the specified index; otherwise, false.</returns>
    public static bool TryGetValue<T>(this T?[,] array, uint x, uint y, out T? value)
    {
        value = array[y, x];
        return value != null;
    }

    /// <summary>
    /// Retrieves a specific row from a two-dimensional array.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the array.</typeparam>
    /// <param name="array">The two-dimensional array.</param>
    /// <param name="row">The index of the row to retrieve.</param>
    /// <returns>An enumerable of elements from the specified row.</returns>
    public static IEnumerable<T?> GetRow<T>(this T?[,] array, uint row)
    {
        for (var i = 0u; i < array.GetLength(1); i++)
            yield return array[row, i];
    }

    /// <summary>
    /// Retrieves the specified column from a two-dimensional array.
    /// </summary>
    /// <typeparam name="T">The type of elements in the array.</typeparam>
    /// <param name="array">The two-dimensional array to retrieve the column from.</param>
    /// <param name="column">The index of the column to retrieve.</param>
    /// <returns>An enumerable of type T containing the elements in the specified column.</returns>
    public static IEnumerable<T?> GetColumn<T>(this T?[,] array, uint column)
    {
        for (var i = 0u; i < array.GetLength(0); i++)
            yield return array[i, column];
    }

    /// <summary>
    /// Finds the elements in a two-dimensional array that satisfy a specified condition and returns them as an enumerable collection.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the array.</typeparam>
    /// <param name="array">The two-dimensional array to search.</param>
    /// <param name="predicate">A function that defines the condition to check against each element of the array.</param>
    /// <returns>An enumerable collection containing the elements from the array that satisfy the specified condition.</returns>
    public static IEnumerable<T?> Find<T>(this T?[,] array, Func<T, bool> predicate)
    {
        for (var i = 0u; i < array.GetLength(0); i++)
        for (var j = 0u; j < array.GetLength(1); j++)
            if (array[i, j] != null && predicate(array[i, j]!))
                yield return array[i, j]!;
    }

    /// <summary>
    /// Searches for elements that match the specified condition in a specified row of a 2D array.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the array.</typeparam>
    /// <param name="array">The 2D array to search in.</param>
    /// <param name="row">The index of the row to search in.</param>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <returns>An enumerable collection of elements that match the condition.</returns>
    public static IEnumerable<T?> FindInRow<T>(this T?[,] array, uint row, Func<T?, bool> predicate)
    {
        for (var i = 0u; i < array.GetLength(1); i++)
            if (array[row, i] != null && predicate(array[row, i]))
                yield return array[row, i];
    }

    /// <summary>
    /// Finds and returns the elements in a specified column of a two-dimensional nullable array that satisfy a given condition.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the array.</typeparam>
    /// <param name="array">The two-dimensional nullable array to search.</param>
    /// <param name="column">The index of the column to search in.</param>
    /// <param name="predicate">A delegate that defines the conditions of the elements to search for.</param>
    /// <returns>
    /// An enumerable collection of elements that satisfy the given condition in the specified column.
    /// </returns>
    public static IEnumerable<T?> FindInColumn<T>(this T?[,] array, uint column, Func<T?, bool> predicate)
    {
        for (var i = 0u; i < array.GetLength(0); i++)
            if (array[i, column] != null && predicate(array[i, column]))
                yield return array[i, column];
    }

    /// <summary>
    /// Finds elements in a 2D array that satisfy a given predicate at specified positions.
    /// </summary>
    /// <typeparam name="T">The type of elements in the array.</typeparam>
    /// <param name="array">The 2D array to search in.</param>
    /// <param name="predicate">A function that determines if an element satisfies a condition.</param>
    /// <param name="positions">The positions in the array to search at.</param>
    /// <returns>An enumerable collection of elements that satisfy the predicate.</returns>
    public static IEnumerable<T?> FindIn<T>(this T?[,] array, Func<T, bool> predicate, params ArrayPosition2D[] positions)
    {
        foreach (var position in positions)
            if (array[position.Row, position.Column] != null && predicate(array[position.Row, position.Column]!))
                yield return array[position.Row, position.Column]!;
    }
}