namespace Lary.Laboratory.Core.Tests;

public class CompasisonHelperTests
{
    public static readonly TheoryData<StringComparison, StringComparer> ToComparerTheoryData = new()
    {
        { StringComparison.CurrentCulture, StringComparer.CurrentCulture },
        { StringComparison.CurrentCultureIgnoreCase, StringComparer.CurrentCultureIgnoreCase },
        { StringComparison.InvariantCulture, StringComparer.InvariantCulture },
        { StringComparison.InvariantCultureIgnoreCase, StringComparer.InvariantCultureIgnoreCase },
        { StringComparison.Ordinal, StringComparer.Ordinal },
        { StringComparison.OrdinalIgnoreCase, StringComparer.OrdinalIgnoreCase }
    };

#pragma warning disable xUnit1045 // Avoid using TheoryData type arguments that might not be serializable
    [Theory, MemberData(nameof(ToComparerTheoryData))]
#pragma warning restore xUnit1045 // Avoid using TheoryData type arguments that might not be serializable
    public void CompasisonHelper_ToComparer_ShouldWork(StringComparison comparison, StringComparer expected)
    {
        ComparisonHelper.ToComparer(comparison)
            .Should().Be(expected);
    }

    [Fact]
    public void CompasisonHelper_ToComparer_InvalidComparison()
    {
        FluentActions.Invoking(() => ComparisonHelper.ToComparer((StringComparison)int.MaxValue))
            .Should().ThrowExactly<NotImplementedException>();
    }
}
