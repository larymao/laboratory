using Lary.Laboratory.Core.Json;

namespace Lary.Laboratory.Core.Tests.Json;

public class BaseFirstContractResolverTests
{
    [Fact]
    public void BaseFirstContractResolver_ShouldWork()
    {
        var obj = new ClassC
        {
            PropA = "A",
            PropB = "B",
            PropC = "C"
        };
        var normalJsonStr = JsonConvert.SerializeObject(obj);

        var jsonStr = JsonConvert.SerializeObject(obj, new JsonSerializerSettings
        {
            ContractResolver = new BaseFirstContractResolver()
        });

        jsonStr.Should().MatchRegex("\"PropA\":.*\"PropB\":.*\"PropC\":.*");
        jsonStr.Length.Should().Be(normalJsonStr.Length);
        jsonStr.Should().NotBe(normalJsonStr);
    }

    internal class ClassA
    {
        public string? PropA { get; set; }
    }

    internal class ClassB : ClassA
    {
        public string? PropB { get; set; }
    }

    internal class ClassC : ClassB
    {
        public string? PropC { get; set; }
    }
}
