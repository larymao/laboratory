using System.Text;

namespace Lary.Laboratory.Core.Tests;

public class StringBuilderHelperTests
{
    public static readonly TheoryData<string, string, bool, string> AppendLineIfTheoryData = new()
    {
        { "hello world", "!", false, "hello world" },
        { "hello world", "!", true, $"hello world!{Environment.NewLine}" }
    };

    [Theory]
    [InlineData("hello world", "!", false, "hello world")]
    [InlineData("hello world", "!", true, "hello world!")]
    public void StringBuilderHelper_AppendIf_ShouldWork(
        string originStr, string txt2Append, bool predicate, string expected)
    {
        var sb = new StringBuilder(originStr);
        var result = sb.AppendIf(predicate, txt2Append);

        result.ToString().Should().Be(expected);
    }

    [Theory, MemberData(nameof(AppendLineIfTheoryData))]
    public void StringBuilderHelper_AppendLineIf_ShouldWork(
        string originStr, string txt2Append, bool predicate, string expected)
    {
        var sb = new StringBuilder(originStr);

        var result = sb.AppendLineIf(predicate, txt2Append);

        result.ToString().Should().Be(expected);
    }
}
