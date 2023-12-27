namespace Frank.Collections.Multidimensional;

public partial class Array2D<T>
{
    public T? Get(ArrayPosition2D position)
    {
        return _array[position.Row, position.Column];
    }

    public T? Get(uint x, uint y)
    {
        return _array[y, x];
    }

    public T?[] GetRow(uint row)
    {
        var rowArray = new T?[Width];
        for (var i = 0u; i < Width; i++) rowArray[i] = _array[row, i];
        return rowArray;
    }

    public T?[] GetColumn(uint column)
    {
        var columnArray = new T?[Height];
        for (var i = 0u; i < Height; i++) columnArray[i] = _array[i, column];
        return columnArray;
    }
}