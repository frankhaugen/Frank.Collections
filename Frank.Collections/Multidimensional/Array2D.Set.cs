namespace Frank.Collections.Multidimensional;

public partial class Array2D<T>
{
    /// <summary>
    /// Sets the value at the specified position in the array.
    /// </summary>
    /// <param name="position">The 2D position in the array to set the value.</param>
    /// <param name="value">The value to set.</param>
    public void Set(ArrayPosition2D position, T value) => _array[position.Row, position.Column] = value;

    /// <summary>
    /// Sets the value of a specified cell in the 2D array.
    /// </summary>
    /// <param name="row">The row index of the cell to be set.</param>
    /// <param name="column">The column index of the cell to be set.</param>
    /// <param name="value">The value to set in the specified cell.</param>
    public void Set(uint row, uint column, T value)
    {
        _array[column, row] = value;
    }
}