namespace Lary.Laboratory.Core.Tests;

public class DateTimeHelperTests
{
    [Theory]
    [InlineData("0", 1970, 1, 1)]
    [InlineData("1640995200", 2022, 1, 1)] // 2022-01-01 00:00:00
    [InlineData("-31536000", 1969, 1, 1)]  // One year before Unix epoch
    [InlineData(null, 1970, 1, 1)]
    [InlineData("invalid", 1970, 1, 1)]
    public void DateTimeHelper_FromUnixTimeSeconds_ShouldWork(string? timestamp, int year, int month, int day)
    {
        var result = DateTimeHelper.FromUnixTimeSeconds(timestamp);

        result.Year.Should().Be(year);
        result.Month.Should().Be(month);
        result.Day.Should().Be(day);
    }

    [Theory]
    [InlineData("0", 1970, 1, 1)]
    [InlineData("1640995200000", 2022, 1, 1)]  // 2022-01-01 00:00:00
    [InlineData("-31536000000", 1969, 1, 1)]   // One year before Unix epoch
    [InlineData(null, 1970, 1, 1)]
    [InlineData("invalid", 1970, 1, 1)]
    public void DateTimeHelper_FromUnixTimeMilliseconds_ShouldWork(string? timestamp, int year, int month, int day)
    {
        var result = DateTimeHelper.FromUnixTimeMilliseconds(timestamp);

        result.Year.Should().Be(year);
        result.Month.Should().Be(month);
        result.Day.Should().Be(day);
    }

    [Theory]
    [InlineData("1640995200", 2022, 1, 1)]     // Seconds format
    [InlineData("1640995200000", 2022, 1, 1)]  // Milliseconds format
    [InlineData(null, 1970, 1, 1)]             // Invalid input returns Unix start
    [InlineData("invalid", 1970, 1, 1)]        // Invalid input returns Unix start
    [InlineData("253402300799", 9999, 12, 31)] // Max valid seconds timestamp
    [InlineData("253402300800", 1978, 1, 11)]  // Treated as milliseconds
    public void DateTimeHelper_FromUnixTimestamp_ShouldWork(string? timestamp, int year, int month, int day)
    {
        var result = DateTimeHelper.FromUnixTimestamp(timestamp);

        result.Year.Should().Be(year);
        result.Month.Should().Be(month);
        result.Day.Should().Be(day);
    }
}