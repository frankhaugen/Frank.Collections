namespace Frank.Collections.Multidimensional;

public partial class Array2D<T>
{
    public void Set(ArrayPosition2D position, T value)
    {
        _array[position.Row, position.Column] = value;
    }

    public void Set(uint row, uint column, T value)
    {
        _array[column, row] = value;
    }
}