using System.Text;

using Frank.Collections.Multidimensional;

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
    /// Converts an IEnumerable of IEnumerable to a 2D array of T?.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source enumerable.</typeparam>
    /// <param name="source">The source enumerable to convert.</param>
    /// <returns>A 2D array of T? representing the source enumerable.</returns>
    public static T?[,] To2DArray<T>(this IEnumerable<IEnumerable<T?>> source)
    {
        var enumerable = source as IEnumerable<T?>[] ?? source.ToArray();
        int height = enumerable.Count();
        int width = enumerable.First().Count();

        T?[,] result = new T?[height, width];

        for (uint y = 0; y < height; y++)
        {
            for (uint x = 0; x < width; x++)
            {
                result[y, x] = enumerable.ElementAt((int)y).ElementAt((int)x);
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

    /// <summary>
    /// Converts a two-dimensional nullable array to a <see cref="Array2D{T}"/> object.
    /// </summary>
    /// <typeparam name="T">The type of elements in the array.</typeparam>
    /// <param name="source">The two-dimensional nullable array to be converted.</param>
    /// <returns>A new instance of <see cref="Array2D{T}"/> containing the elements from the source array.</returns>
    public static Array2D<T?> ToArray2D<T>(this T?[,] source) => new(source);
}