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

#pragma warning disable xUnit1045 // Avoid using TheoryData type arguments that might not be serializable
    [Theory, MemberData(nameof(PropertiesEqualTheoryData))]
#pragma warning restore xUnit1045 // Avoid using TheoryData type arguments that might not be serializable
    public void ObjectComparer_PropertiesEqual_ShouldWork(
        Foo obj1, Foo obj2, string[] ignores, bool expected)
    {
        ObjectComparer.PropertiesEqual(obj1, obj2, ignores)
            .Should().Be(expected);
    }

    public record class Foo(string? PropA, int PropB, double PropC);
}
