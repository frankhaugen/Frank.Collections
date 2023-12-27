using System.Text;

namespace Frank.Collections.Multidimensional;

public partial class Array2D<T>
{
    private readonly T?[,] _array;

    public uint Width { get; private set; }
    public uint Height { get; private set; }

    public Array2D(uint width, uint height)
    {
        Width = width;
        Height = height;
        _array = new T?[width, height];
    }

    public string GetMap()
    {
        var map = new bool[Width, Height];
        for (var i = 0u; i < Width; i++)
        for (var j = 0u; j < Height; j++)
            if (_array.TryGetValue(new ArrayPosition2D(i, j), out var value) && value != null)
                map[i, j] = true;
            else
                map[i, j] = false;

        var stringBuilder = new StringBuilder();
        for (var i = 0u; i < Width; i++)
        {
            for (var j = 0u; j < Height; j++)
                stringBuilder.Append(map[i, j] ? "X" : " ");
            stringBuilder.Append(Environment.NewLine);
        }

        return stringBuilder.ToString();
    }

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();
        for (var i = 0u; i < Width; i++)
        {
            for (var j = 0u; j < Height; j++)
                stringBuilder.Append($"[{this[i, j]}] ");
            stringBuilder.Append('\n');
        }

        return stringBuilder.ToString();
    }
}