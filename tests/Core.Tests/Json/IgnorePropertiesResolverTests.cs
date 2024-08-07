using Lary.Laboratory.Core.Json;

namespace Lary.Laboratory.Core.Tests.Json;

public class IgnorePropertiesResolverTests
{
    [Fact]
    public void IgnorePropertiesResolver_ShouldWork()
    {
        var expected = JsonConvert.SerializeObject(new { PropB = "hello" });

        var obj = new
        {
            PropA = 1,
            PropB = "hello"
        };
        var resolver = new IgnorePropertiesResolver(["PropA"]);
        var result = JsonConvert.SerializeObject(obj, new JsonSerializerSettings { ContractResolver = resolver});

        result.Should().Be(expected);
    }
}
