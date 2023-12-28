using Frank.Collections.Multidimensional;
using Frank.Collections.Serialization;

namespace Frank.Collections.Tests.Serialization;

public partial class Array2DSerializerTests
{
    public static IEnumerable<object[]> RoundTripData =>
        new List<object[]> { new object[] { new int?[,] { { 1, 2 }, { 3, 4 } } }, new object[] { new int?[,] { { null, 2 }, { 3, 4 } } }, new object[] { new string?[,] { { "aa", "bb" }, { "cc", "dd" } } } };

    [Theory]
    [MemberData(nameof(RoundTripData))]
    public void RoundTripSerialization_ShouldReturnOriginalValue<T>(T[,] value)
    {
        // Arrange
        var array2D = new Array2D<T>(value);

        // Act
        var serialized = Array2DSerializer.Serialize(array2D);
        var deserialized = Array2DSerializer.Deserialize<T>(serialized);

        // Assert
        Assert.NotNull(deserialized);
        Assert.Equal(array2D, deserialized!, new Array2DComparer<T>());
    }
}