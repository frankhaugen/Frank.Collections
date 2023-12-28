using Frank.Collections.Multidimensional;
using Frank.Collections.Serialization;

namespace Frank.Collections.Tests.Serialization;

public partial class Array2DSerializerTests
{
    public static IEnumerable<object[]> DeserializeData =>
        new List<object[]> { new object[] { "[1] [2]\n[3] [4]", new Array2D<int?>(new int?[,] { { 1, 2 }, { 3, 4 } }) }, new object[] { "[] [2]\n[3] [4]", new Array2D<int?>(new int?[,] { { null, 2 }, { 3, 4 } }) }, new object[] { "[\"aa\"] [\"bb\"]\n[\"cc\"] [\"dd\"]", new Array2D<string?>(new string?[,] { { "aa", "bb" }, { "cc", "dd" } }) } };

    [Theory]
    [MemberData(nameof(DeserializeData))]
    public void Deserialize_ShouldReturnExpectedValue<T>(string value, Array2D<T> expectedArray)
    {
        // Arrange

        // Act
        var result = Array2DSerializer.Deserialize<T>(value);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedArray, result!, new Array2DComparer<T>());
    }
}