namespace Frank.Collections.Multidimensional;

// File src/Frank.GameEngine.Primitives/SubPrimitives/Array2D.Indexers.cs
public partial class Array2D<T>
{
    public T? this[ArrayPosition2D position]
    {
        get => _array[position.Row, position.Column];
        set => _array[position.Row, position.Column] = value;
    }

    public T? this[uint row, uint column]
    {
        get => _array[row, column];
        set => _array[row, column] = value;
    }
}