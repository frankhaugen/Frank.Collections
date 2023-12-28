using System.Text;

namespace Frank.Collections.Extensions;

public static class ArrayExtensions
{
    /// <summary>
    /// Selects and maps each element from a 2D array to a new array
    /// by applying the specified selector function.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements in the source array.</typeparam>
    /// <typeparam name="TResult">The type of the elements in the resulting array.</typeparam>
    /// <param name="source">The 2D array from which to select and map elements.</param>
    /// <param name="selector">A function to transform the elements from the source array.</param>
    /// <returns>A new 2D array with the transformed elements.</returns>
    public static TResult[,] Select2D<TSource, TResult>(this TSource[,] source, Func<TSource, TResult> selector)
    {
        int height = source.GetLength(0);
        int width = source.GetLength(1);

        TResult[,] result = new TResult[height, width];

        for (uint y = 0; y < height; y++)
        {
            for (uint x = 0; x < width; x++)
            {
                result[y, x] = selector(source[y, x]);
            }
        }

        return result;
    }

    /// <summary>
    /// Converts a jagged array to a two-dimensional array.
    /// </summary>
    /// <typeparam name="T">The type of elements in the array.</typeparam>
    /// <param name="source">The jagged array to convert.</param>
    /// <returns>A two-dimensional array containing the elements from the jagged array.</returns>
    public static T?[,] To2DArray<T>(this T?[][] source)
    {
        int height = source.Length;
        int width = source[0].Length;

        T?[,] result = new T?[height, width];

        for (uint y = 0; y < height; y++)
        {
            for (uint x = 0; x < width; x++)
            {
                result[y, x] = source[y][x];
            }
        }

        return result;
    }

    /// <summary>
    /// Returns a string representation of the 2D array of nullable strings.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="newLine">The character to use as a new line. Defaults to '\n'. An alternative is '\r\n'. Warning: This method does not check for the new line character in the strings of the array, nor does it check if the new line character is an actual new line character.</param>
    /// <returns>A single string containing all the values of the 2D array.</returns>
    public static string AsString(this string?[,] source, char newLine = '\n')
    {
        var stringBuilder = new StringBuilder();
        for (var i = 0u; i < source.GetLength(0); i++)
        {
            for (var j = 0u; j < source.GetLength(1); j++)
                stringBuilder.Append($"[{source[i, j]}] ");
            stringBuilder.Remove(stringBuilder.Length - 1, 1);
            stringBuilder.Append(newLine);
        }
        stringBuilder.Remove(stringBuilder.Length - 1, 1);
        return stringBuilder.ToString();
    }
}