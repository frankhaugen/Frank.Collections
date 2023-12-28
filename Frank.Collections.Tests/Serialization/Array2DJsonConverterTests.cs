using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

using Frank.Collections.Multidimensional;
using Frank.Collections.Serialization;

namespace Frank.Collections.Tests.Serialization;

[TestSubject(typeof(Array2DJsonConverter<TestItem>))]
public class Array2DJsonConverterTests(ITestOutputHelper outputHelper)
{
    private readonly ITestOutputHelper _outputHelper = outputHelper;

    [Fact]
    public void TestSerialize()
    {
        // Arrange
        var expected = """[[{"name":"test1","age":10},{"name":"test2","age":20}],[{"name":"test3","age":30},{"name":"test4","age":40}]]""";
        var options = new JsonSerializerOptions
        {
            WriteIndented = false,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
            ReadCommentHandling = JsonCommentHandling.Skip,
            Converters = { new JsonStringEnumConverter(), new Array2DJsonConverter<TestItem>() },
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
        var array2D = new Array2D<TestItem>(2, 2)
        {
            [0, 0] = new TestItem { Name = "test1", Age = 10 },
            [0, 1] = new TestItem { Name = "test2", Age = 20 },
            [1, 0] = new TestItem { Name = "test3", Age = 30 },
            [1, 1] = new TestItem { Name = "test4", Age = 40 }
        };
        
        // Act
        var json = JsonSerializer.Serialize(array2D, options);
        _outputHelper.WriteLine(json);
        _outputHelper.WriteLine(expected);
        
        // Assert
        Assert.Equal(expected, json);
    }
    
    [Fact]
    public void TestDeserialize()
    {
        // Arrange
        var expected = new Array2D<TestItem>(2, 2)
        {
            [0, 0] = new TestItem { Name = "test1", Age = 10 },
            [0, 1] = new TestItem { Name = "test2", Age = 20 },
            [1, 0] = new TestItem { Name = "test3", Age = 30 },
            [1, 1] = new TestItem { Name = "test4", Age = 40 }
        };
        var options = new JsonSerializerOptions
        {
            WriteIndented = false,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
            ReadCommentHandling = JsonCommentHandling.Skip,
            Converters = { new JsonStringEnumConverter(), new Array2DJsonConverter<TestItem>() },
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
        var json = """[[{"name":"test1","age":10},{"name":"test2","age":20}],[{"name":"test3","age":30},{"name":"test4","age":40}]]""";
        
        // Act
        var array2D = JsonSerializer.Deserialize<Array2D<TestItem>>(json, options);
        
        // Assert
        Assert.Equal(expected, array2D);
    }
    
    private record struct TestItem
    {
        public string? Name { get; set; }
        public int Age { get; set; }
    }
}