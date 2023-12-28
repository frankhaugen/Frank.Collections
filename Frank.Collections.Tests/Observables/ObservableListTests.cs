using Frank.Collections.Observables;

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Frank.Collections.Tests.Observables;

[TestSubject(typeof(ObservableList<TestItem>))]
public class ObservableListTests
{
    [Fact]
    public void TestAdd()
    {
        var ol = new ObservableList<TestItem>();
        var item = new TestItem { Name = "test" };
        ol.Add(item);
        Assert.Single(ol);
        Assert.Equal(item, ol[0]);
    }

    [Fact]
    public void TestClear()
    {
        var ol = new ObservableList<TestItem> { new TestItem() { Name = "test1", Age = 10 }, new TestItem() { Name = "test2", Age = 20 }, new TestItem() { Name = "test3", Age = 30 } };
        ol.Clear();
        Assert.Empty(ol);
    }

    [Fact]
    public void TestContains_True()
    {
        var item = new TestItem { Name = "test" };
        var ol = new ObservableList<TestItem> { item };
        Assert.Contains(item, ol);
    }

    [Fact]
    public void TestContains_False()
    {
        var ol = new ObservableList<TestItem>();
        Assert.DoesNotContain(new TestItem() { Name = "test" }, ol);
    }

    [Fact]
    public void TestCopyTo()
    {
        var item = new TestItem { Name = "test" };
        var ol = new ObservableList<TestItem> { item };
        var arr = new TestItem[1];
        ol.CopyTo(arr, 0);
        Assert.Equal(item, arr[0]);
    }

    [Fact]
    public void TestRemove_True()
    {
        var item = new TestItem { Name = "test" };
        var ol = new ObservableList<TestItem> { item };
        Assert.True(ol.Remove(item));
    }

    [Fact]
    public void TestRemove_False()
    {
        var ol = new ObservableList<TestItem>();
        Assert.False(ol.Remove(new TestItem() { Name = "test" }));
    }

    [Fact]
    public void TestIndexOf_Exists()
    {
        var item = new TestItem { Name = "test" };
        var ol = new ObservableList<TestItem> { item };
        Assert.Equal(0, ol.IndexOf(item));
    }

    [Fact]
    public void TestIndexOf_NotExists()
    {
        var ol = new ObservableList<TestItem>();
        Assert.Equal(-1, ol.IndexOf(new TestItem() { Name = "test" }));
    }

    [Fact]
    public void TestInsert()
    {
        var ol = new ObservableList<TestItem>();
        var item = new TestItem { Name = "test" };
        ol.Insert(0, item);
        Assert.Single(ol);
        Assert.Equal(item, ol[0]);
    }

    [Fact]
    public void TestRemoveAt()
    {
        var item = new TestItem { Name = "test" };
        var ol = new ObservableList<TestItem> { item };
        ol.RemoveAt(0);
        Assert.Empty(ol);
    }

    [Fact]
    public void TestItemGetterAndSetter()
    {
        var ol = new ObservableList<TestItem> { new TestItem { Name = "test" } };
        ol[0] = new TestItem { Name = "newTest" };
        Assert.Equal("newTest", ol[0].Name);
    }
    
    [Fact]
    public void TestChangeNotification()
    {
        var observer = new TestObserver<TestItem>();
        var ol = new ObservableList<TestItem> { new() { Name = "test" } };
        using var unsubscriber = ol.Subscribe(observer);
        var item = new TestItem { Name = "newTest" };
        var item2 = new TestItem { Name = "test2" };
        
        ol.Insert(0, item);
        Assert.Equal(item, observer.Value);
        
        ol[0] = item2;
        Assert.Equal(item2, observer.Value);
        
        ol.Remove(item2);
        Assert.Null(observer.Value);
        
        ol.Add(item);
        item.Name = "newTest2";
        Assert.NotNull(observer.Value);
        Assert.Equal("newTest2", observer.Value.Name);
    }
}

file class TestItem : INotifyPropertyChanged
{
    private string? _name;
    private int _age;

    public string? Name
    {
        get => _name;
        set => SetField(ref _name, value);
    }

    public int Age
    {
        get => _age;
        set => SetField(ref _age, value);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}

file class TestObserver<T> : IObserver<T>
{
    public T? Value { get; private set; }
    
    public void OnCompleted()
    {
        Value = default;
    }

    public void OnError(Exception error)
    {
        Console.Error.WriteLine($"An error occurred: {error.Message}");
    }

    public void OnNext(T value)
    {
        Value = value;
    }
}