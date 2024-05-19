namespace Lary.Laboratory.Core.Tests;

public class StringHelperTests
{
    [Theory]
    [InlineData("\n\nhello \n\tworld\n", "hello \n\tworld")]
    [InlineData("\n    hello \n    world", "hello \nworld")]
    public void StringHelper_TrimIndents_ShouldWork(string src, string expected)
    {
        var result = StringHelper.TrimIndents(src);

        Assert.Equal(expected, result);
    }
}
