using Lary.Laboratory.Core.Utils;

namespace Lary.Laboratory.Core.Tests.Utils;

public class ObjectComparerTests
{
    public static readonly TheoryData<Foo, Foo, string[], bool> PropertiesEqualTheoryData = new()
    {
        { new Foo("helllo world", 1, 1.23), new Foo("helllo world", 1, 1.23), [], true },
        { new Foo(string.Empty, 1, 1.23), new Foo(null, 1, 1.23), [], false },
        { new Foo(null, 1, 1.23), new Foo(null, 1, 1.23), [], true }
    };

    [Theory, MemberData(nameof(PropertiesEqualTheoryData))]
    public void ObjectComparer_PropertiesEqual_ShouldWork(
        Foo obj1, Foo obj2, string[] ignores, bool expected)
    {
        ObjectComparer.PropertiesEqual(obj1, obj2, ignores)
            .Should().Be(expected);
    }

#if NET7_0_OR_GREATER
    public record class Foo(string? PropA, int PropB, double PropC);
#else
    public class Foo(string? propA, int propB, double propC)
    {
        public string? PropA { get; } = propA;
        public int PropB { get; } = propB;
        public double PropC { get; } = propC;
    }
#endif
}
