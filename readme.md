# Frank.Collections
2D arrays, observables and other specialized collections

## 2D Arrays

### Array2D

A 2D array that allows for serialization and deserialization to and from JSON and its own string representation with the use of the Array2DSerializer static class.

```csharp
var array = new Array2D<int>(2, 2);
array[0, 0] = 1;
array[0, 1] = 2;
array[1, 0] = 3;
array[1, 1] = 4;

foreach (var item in array)
{
    Console.WriteLine(item);
}
```

## Observables

### ObservableList

A list that can be observed for changes, such as adding and removing items, but also the property values of the items themselves. Warning: This is a very expensive feature, so use it sparingly, so if you don't need it, use the regular List class.

```csharp
var list = new ObservableList<int>();
list.CollectionChanged += (sender, e) => Console.WriteLine($"Item added: {e.Item}");
list.Add(1);
list.Add(2);
list.Add(3);
list.Add(4);

foreach (var item in list)
{
    Console.WriteLine(item);
}
```

## Installation

### NuGet

```bash
dotnet add package Frank.Collections
```

## License

[MIT](LICENSE)