using Frank.Collections.Serialization;

namespace Frank.Collections.Tests.Serialization;

[TestSubject(typeof(Array2DSerializer))]
public partial class Array2DSerializerTests(ITestOutputHelper outputHelper)
{
    private readonly ITestOutputHelper _outputHelper = outputHelper;
}
