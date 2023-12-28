using System.Collections.Specialized;
using System.ComponentModel;

namespace Frank.Collections.Observables;

/// <summary>
/// Represents an observable list that implements the <see cref="IList{T}"/>, <see cref="INotifyCollectionChanged"/>,
/// and <see cref="IObservable{T}"/> interfaces. The elements in this list are of type T and
/// must implement the <see cref="INotifyPropertyChanged"/> interface.
/// </summary>
/// <typeparam name="T">The type of elements in the list.</typeparam>
public interface IObservableList<T> : IList<T>, INotifyCollectionChanged, IObservable<T>
    where T : INotifyPropertyChanged
{
    /// <summary>
    /// Adds a collection of items to the current collection.
    /// </summary>
    /// <param name="items">The collection of items to add.</param>
    void AddRange(IEnumerable<T> items);

    /// <summary>
    /// Removes the specified range of items from the collection.
    /// </summary>
    /// <typeparam name="T">The type of the items in the collection.</typeparam>
    /// <param name="items">The range of items to be removed.</param>
    void RemoveRange(IEnumerable<T> items);

    /// <summary>
    /// Asynchronously adds an item of type T to the collection.
    /// </summary>
    /// <param name="item">The item to be added.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task AddAsync(T item);

    /// <summary>
    /// Removes the specified item asynchronously.
    /// </summary>
    /// <param name="item">The item to be removed.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task RemoveAsync(T item);

    /// <summary>
    /// Filters the elements in the observable list based on the specified predicate.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <param name="predicate">A function that determines whether an element should be included in the filtered list.</param>
    /// <returns>An observable list containing the filtered elements.</returns>
    IObservableList<T> Filter(Func<T, bool> predicate);

    /// <summary>
    /// Sorts the elements in the entire list using the specified comparison function. </summary>
    /// <param name="comparison">
    /// A comparison function that defines the sort order. The comparison function should
    /// return a negative integer if the first argument is less than the second argument,
    /// zero if the arguments are equal, and a positive integer if the first argument is
    /// greater than the second argument.
    /// </param>
    void Sort(Comparison<T> comparison);

    /// <summary>
    /// Gets the number of subscribers.
    /// </summary>
    int SubscriberCount { get; }

    /// <summary>
    /// Gets or sets the element at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the element to get or set.</param>
    /// <returns>The element at the specified index.</returns>
    /// <typeparam name="T">The type of the elements in the collection.</typeparam>
    new T this[int index] { get; set; }
}