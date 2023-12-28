using Frank.Collections.Multidimensional;
using Frank.Collections.Serialization;

namespace Frank.Collections.Tests.Serialization;

public partial class Array2DSerializerTests
{
    public static IEnumerable<object[]> SerializeData =>
        new List<object[]> { new object[] { new int?[,] { { 1, 2 }, { 3, 4 } }, "[1] [2]\n[3] [4]" }, new object[] { new int?[,] { { null, 2 }, { 3, 4 } }, "[] [2]\n[3] [4]" } };

    [Theory]
    [MemberData(nameof(SerializeData))]
    public void Serialize_ShouldReturnExpectedValue(int?[,] value, string expectedJson)
    {
        // Arrange
        var array2D = new Array2D<int?>(value);

        // Act
        var result = Array2DSerializer.Serialize(array2D);

        // Assert
        Assert.Equal(expectedJson, result);
    }
}