using static Lary.Laboratory.Core.Tests.DictionaryHelperTests;

namespace Lary.Laboratory.Core.Tests;

public class DictionaryHelperTests
{
    private static readonly Foo _fooObj = new()
    {
        PropA = 1,
        PropB = 2.4,
        PropC = Guid.NewGuid().ToString(),
        PropD = null,
        PropE = [Guid.NewGuid().ToString(), Guid.NewGuid().ToString()],
        PropF = new() { { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() } },
        PropG = new InnerFoo
        {
            InnerPropA = 11,
            InnerPropB = nameof(InnerFoo.InnerPropB)
        }
    };

    private static readonly Dictionary<string, object?> _fooDic = new()
    {
        { nameof(Foo.PropA), _fooObj.PropA },
        { nameof(Foo.PropB), _fooObj.PropB },
        { nameof(Foo.PropC), _fooObj.PropC },
        { nameof(Foo.PropD), _fooObj.PropD },
        { nameof(Foo.PropE), _fooObj.PropE },
        { nameof(Foo.PropF), _fooObj.PropF },
        { nameof(Foo.PropG), _fooObj.PropG }
    };

    [Fact]
    public void DictionaryHelper_ToObject_ShouldWork()
    {
        var expected = _fooObj;

        var result = DictionaryHelper<Foo>.ToObject(_fooDic);

        JsonConvert.SerializeObject(result)
            .Should().Be(JsonConvert.SerializeObject(expected));
    }

    [Fact]
    public void DictionaryHelper_FromObject_ShouldWork()
    {
        var expected = _fooDic;

        var result = DictionaryHelper<Foo>.FromObject(_fooObj);

        JsonConvert.SerializeObject(result)
            .Should().Be(JsonConvert.SerializeObject(expected));
    }

    [Fact]
    public void DictionaryHelper_ToGeneric_ShouldWork()
    {
        var expected = _fooDic.ToDictionary(x => x.Key, x => x.Value?.ToString());

        var result = DictionaryHelper.ToGeneric(_fooDic, key => key.ToString()!, val => val?.ToString());

        JsonConvert.SerializeObject(result)
            .Should().Be(JsonConvert.SerializeObject(expected));
    }

    internal class Foo
    {
        public int PropA { get; set; }

        public double PropB { get; set; }

        public string? PropC { get; set; }

        public string? PropD { get; set; }

        public string?[]? PropE { get; set; }

        public Dictionary<string, string?>? PropF { get; set; }

        public InnerFoo? PropG { get; set; }
    }

    internal class InnerFoo
    {
        public int InnerPropA { get; set; }

        public string? InnerPropB { get; set; }
    }
}
