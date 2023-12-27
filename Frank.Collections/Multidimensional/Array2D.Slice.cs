namespace Frank.Collections.Multidimensional;

public partial class Array2D<T>
{
    public Array2D<T> Slice(uint x, uint y, uint width, uint height)
    {
        var slice = new Array2D<T>(width, height);
        for (var i = x; i < x + width; i++)
        for (var j = y; j < y + height; j++)
            slice[i - x, j - y] = _array[i, j];
        return slice;
    }
}