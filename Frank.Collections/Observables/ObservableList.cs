using System.Collections.Specialized;
using System.ComponentModel;

using Frank.Collections.Internals;

namespace Frank.Collections.Observables;

public class ObservableList<T> : List<T>, IObservableList<T> where T : INotifyPropertyChanged
{
    private readonly List<IObserver<T>> _observers = new();

    public ObservableList() => CollectionChanged += OnCollectionChanged;

    public new void Add(T item)
    {
        base.Add(item);
        SubscribeToItemPropertyChanged(item);
        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
    }

    public new void Clear()
    {
        base.Clear();
        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }

    public new bool Remove(T item)
    {
        if(base.Remove(item))
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
            return true;
        }
        UnsubscribeFromItemPropertyChanged(item);
        return false;
    }

    public new void Insert(int index, T item)
    {
        base.Insert(index, item);
        SubscribeToItemPropertyChanged(item);
        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
    }

    public new void RemoveAt(int index)
    {
        T item = base[index];
        base.RemoveAt(index);
        UnsubscribeFromItemPropertyChanged(item);
        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index));
    }

    public new void AddRange(IEnumerable<T> itemsToAdd)
    {
        itemsToAdd = itemsToAdd.ToArray();
        base.AddRange(itemsToAdd);
        foreach (T item in itemsToAdd)
        {
            SubscribeToItemPropertyChanged(item);
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
        }
    }

    public void RemoveRange(IEnumerable<T> itemsToRemove)
    {
        foreach (T item in itemsToRemove)
        {
            base.Remove(item);
            UnsubscribeFromItemPropertyChanged(item);
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
        }
    }

    public new T this[int index]
    {
        get => base[index];
        set
        {
            T oldItem = base[index];
            base[index] = value;
            SubscribeToItemPropertyChanged(base[index]);
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, value, oldItem, index));
        }
    }

    public event NotifyCollectionChangedEventHandler? CollectionChanged;
        
    public IDisposable Subscribe(IObserver<T> observer)
    {
        if (!_observers.Contains(observer))
        {
            _observers.Add(observer);
        }
        return new Unsubscriber<T>(_observers, observer);
    }

    public async Task AddAsync(T item)
    {
        await Task.Run(() => this.Add(item));
    
        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
    }

    public async Task RemoveAsync(T item)
    {
        await Task.Run(() => this.Remove(item));
        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
    }

    public IObservableList<T> Filter(Func<T, bool> predicate)
    {
        var filteredList = new ObservableList<T>();
        foreach (var item in this.Where(predicate)) filteredList.Add(item);
        return filteredList;
    }

    public new void Sort(Comparison<T> comparison)
    {
        base.Sort(comparison);
        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }

    public int SubscriberCount => _observers.Count;

    public override int GetHashCode() => this.Aggregate(0, (current, item) => current ^ item.GetHashCode());
    
    public override bool Equals(object? obj)
    {
        if (obj is not ObservableList<T> list) return false;
        if (list.Count != Count) return false;
        return GetHashCode() == list.GetHashCode();
    }

    private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        foreach (var observer in _observers)
        {
            var newItems = e.NewItems?.Cast<T>();
            var oldItems = e.OldItems?.Cast<T>();
            
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    newItems?.DoForEach(item => observer.OnNext(item));
                    break;
                case NotifyCollectionChangedAction.Remove:
                    oldItems?.DoForEach(item => observer.OnCompleted());
                    break;
                case NotifyCollectionChangedAction.Replace:
                    newItems?.DoForEach(item => observer.OnNext(item));
                    break;
                case NotifyCollectionChangedAction.Reset:
                case NotifyCollectionChangedAction.Move:
                default:
                    break;
            }
        }
    }
    
    private void SubscribeToItemPropertyChanged(T? item)
    {
        if (item != null)
            item.PropertyChanged += OnItemPropertyChanged;
    }

    private void UnsubscribeFromItemPropertyChanged(T? item)
    {
        if (item != null)
            item.PropertyChanged -= OnItemPropertyChanged;
    }
    
    private void OnItemPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        // Cast the sender to T
        T item = (T)sender!;
    
        // Check if the item is in the list
        if (this.Contains(item))
        {
            // Notify each observer about the change
            foreach (var observer in _observers)
            {
                observer.OnNext(item);
            }
        }
        else
        {
            // If the item is no longer in the list, unsubscribe from its PropertyChanged event
            UnsubscribeFromItemPropertyChanged(item);
        }
    }
}