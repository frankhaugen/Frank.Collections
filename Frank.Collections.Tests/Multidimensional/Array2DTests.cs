using Xunit;

using Frank.Collections.Multidimensional;

using JetBrains.Annotations;

using System;

namespace Frank.Collections.Tests.Multidimensional;

[TestSubject(typeof(Array2D<int>))]
public class Array2DTests
{
    [Fact]
    public void TestArray2DConstructor()
    {
        var array2D = new Array2D<int>(2, 3);
        Assert.NotNull(array2D);
        Assert.Equal(2u, array2D.Width);
        Assert.Equal(3u, array2D.Height);
    }

    [Fact]
    public void TestArray2DSetAndGet()
    {
        var array2D = new Array2D<int>(2, 2);
        array2D.Set(new ArrayPosition2D(1, 1), 5);
        array2D.Set(0, 1, 7);
        var result = array2D.Get(new ArrayPosition2D(1, 1));
        var result2 = array2D.Get(0, 1);

        Assert.Equal(5, result);
        Assert.Equal(7, result2);
    }

    [Fact]
    public void TestArray2DGetterAndSetter()
    {
        var array2D = new Array2D<int>(2, 2);
        array2D[1, 1] = 5;
        array2D[new ArrayPosition2D(0, 1)] = 7;
        var result = array2D[1, 1];
        var result2 = array2D[new ArrayPosition2D(0, 1)];

        Assert.Equal(5, result);
        Assert.Equal(7, result2);
    }

    [Fact]
    public void TestArray2DSlice()
    {
        var array2D = new Array2D<int>(3, 3);
        array2D[1, 1] = 5;
        var resultSlice = array2D.Slice(1, 1, 1, 1);
        var result = resultSlice[0, 0];

        Assert.Equal(5, result);
    }

    [Fact]
    public void TestArray2DToString()
    {
        var array2D = new Array2D<int>(2, 2);
        array2D[1, 1] = 5;
        var result = array2D.ToString();

        Assert.Equal("[0] [0] \n[0] [5] \n", result);
    }

    [Theory]
    [InlineData(1, 1, 5)]
    [InlineData(0, 1, 7)]
    public void TestArray2DSet(uint x, uint y, int value)
    {
        var array2D = new Array2D<int>(2, 2);
        array2D.Set(x, y, value);
        var result = array2D.Get(x, y);

        Assert.Equal(value, result);
    }

    [Fact]
    public void TestArray2DFind()
    {
        var array2D = new Array2D<int>(3, 3);
        array2D[1, 1] = 5;
        array2D[0, 1] = 7;
        var result = array2D.Find(x => x == 5);

        Assert.Single(result, 5);
    }

    [Fact]
    public void TestArray2DFindInRow()
    {
        var array2D = new Array2D<int>(3, 3);
        array2D[1, 1] = 5;
        array2D[0, 1] = 7;
        var result = array2D.FindInRow(1, x => x == 5);

        Assert.Single(result, 5);
    }

    [Fact]
    public void TestArray2DFindInColumn()
    {
        var array2D = new Array2D<int>(3, 3);
        array2D[1, 1] = 5;
        array2D[0, 1] = 7;
        var result = array2D.FindInColumn(1, x => x == 7);

        Assert.Single(result, 7);
    }
}