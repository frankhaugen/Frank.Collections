namespace Frank.Collections.Multidimensional;

// File src/Frank.GameEngine.Primitives/SubPrimitives/Array2D.Find.cs
public partial class Array2D<T>
{
    public IEnumerable<T?> Find(Func<T, bool> predicate)
    {
        return _array.Find(predicate);
    }

    public IEnumerable<T?> FindInRow(uint row, Func<T?, bool> predicate)
    {
        return GetRow(row).Where(predicate);
    }

    public IEnumerable<T?> FindInColumn(uint column, Func<T?, bool> predicate)
    {
        return GetColumn(column).Where(predicate);
    }

    public IEnumerable<T?> FindIn(Func<T, bool> predicate, params ArrayPosition2D[] positions)
    {
        return _array.FindIn(predicate, positions);
    }
}