namespace Frank.Collections.Internals;

internal class Unsubscriber<T>(ICollection<IObserver<T>>? observers, IObserver<T>? observer) : IDisposable
{
    public void Dispose()
    {
        if (observers != null && observer != null && observers.Contains(observer)) observers.Remove(observer);
    }
}