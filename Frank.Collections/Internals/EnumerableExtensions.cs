namespace Frank.Collections.Internals;

public static class EnumerableExtensions
{
    /// <summary>
    /// Filters out null values from the given enumerable sequence.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the enumerable sequence.</typeparam>
    /// <param name="source">The enumerable sequence to filter.</param>
    /// <returns>An enumerable sequence that contains all the non-null elements from the original sequence.</returns>
    public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> source) where T : class => source.Where(item => item != null)!;

    /// <summary>
    /// Asynchronously applies the specified asynchronous function to each element in the source collection and returns the results as an enumerable.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source collection.</typeparam>
    /// <param name="source">The source collection to apply the function on.</param>
    /// <param name="func">The asynchronous function to apply on each element.</param>
    /// <returns>
    /// An enumerable containing the results of applying the specified asynchronous function to each element in the source collection.
    /// </returns>
    public static async Task<IEnumerable<T>> DoForEachAsync<T>(this IEnumerable<T> source, Func<T, Task<T>> func) => await Task.WhenAll(source.Select(func));

    /// <summary>
    /// Applies a specified function to each element in the source collection and returns the result. It also passes the source collection to the function to allow for contextual operations on the source collection.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="source">The source collection.</param>
    /// <param name="func">The function to apply to each element.</param>
    /// <returns>A new collection containing the result of applying the function to each element in the source collection.</returns>
    public static IEnumerable<T> DoForEach<T>(this IEnumerable<T> source, Func<IEnumerable<T>, T, T> func) => source.Select(item => func(source, item));

    /// <summary>
    /// Invokes the specified action on each element of the source collection and returns the unchanged source collection.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source collection.</typeparam>
    /// <param name="source">The source collection.</param>
    /// <param name="action">The action to be invoked on each element.</param>
    /// <returns>The unchanged source collection.</returns>
    public static IEnumerable<T> DoForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        // ReSharper disable PossibleMultipleEnumeration
        foreach (T item in source) action(item);
        return source;
    }

    /// <summary>
    /// Performs an action on each element in the source collection and returns the same collection.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="source">The source collection.</param>
    /// <param name="action">The action to perform on each element.</param>
    /// <returns>Returns the same collection after performing the action on each element.</returns>
    public static IEnumerable<T> DoForEach<T>(this IEnumerable<T> source, Func<T, T> action) => source.Select(action);

    /// <summary>
    /// Transforms each element of the source collection into a new element by applying the specified function.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements in the source collection.</typeparam>
    /// <typeparam name="TResult">The type of the elements in the resulting collection.</typeparam>
    /// <param name="source">The source collection.</param>
    /// <param name="func">A function that defines the transformation logic for each element.</param>
    /// <returns>A collection of elements of type TResult resulting from the transformation operation.</returns>
    public static IEnumerable<TResult> TransformEach<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> func) => source.Select(func);
    
}