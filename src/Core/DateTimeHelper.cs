namespace Lary.Laboratory.Core;

public static class DateTimeHelper
{
    /// <summary>
    /// Unix timestamp start time
    /// </summary>
    private static readonly DateTimeOffset UnixStart = new(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);

    /// <summary>
    /// Converts a Unix time expressed as the number of seconds that have elapsed since
    /// 1970-01-01T00:00:00Z to a System.DateTimeOffset value.
    /// </summary>
    /// <param name="timestamp">
    /// A Unix time, expressed as the number of seconds that have elapsed since 1970-01-01T00:00:00Z
    /// (January 1, 1970, at 12:00 AM UTC). For Unix times before this date, its value is negative.
    /// </param>
    /// <returns>A date and time value that represents the same moment in time as the Unix time.</returns>
    public static DateTimeOffset FromUnixTimeSeconds(string? timestamp)
    {
        return long.TryParse(timestamp, out long seconds)
            ? DateTimeOffset.FromUnixTimeSeconds(seconds)
            : UnixStart;
    }

    /// <summary>
    /// Converts a Unix time expressed as the number of milliseconds that have elapsed since
    /// 1970-01-01T00:00:00Z to a System.DateTimeOffset value.
    /// </summary>
    /// <param name="timestamp">
    /// A Unix time, expressed as the number of milliseconds that have elapsed since 1970-01-01T00:00:00Z
    /// (January 1, 1970, at 12:00 AM UTC). For Unix times before this date, its value is negative.
    /// </param>
    /// <returns>A date and time value that represents the same moment in time as the Unix time.</returns>
    public static DateTimeOffset FromUnixTimeMilliseconds(string? timestamp)
    {
        return long.TryParse(timestamp, out long milliseconds)
            ? DateTimeOffset.FromUnixTimeMilliseconds(milliseconds)
            : UnixStart;
    }

    /// <summary>
    /// Converts a Unix timestamp string to a System.DateTimeOffset value. This method automatically
    /// determines whether the timestamp represents seconds or milliseconds based on its magnitude.
    /// </summary>
    /// <param name="timestamp">
    /// A Unix timestamp string that represents either seconds or milliseconds since 1970-01-01T00:00:00Z.
    /// For Unix times before this date, its value is negative.
    /// </param>
    /// <returns>
    /// A date and time value that represents the same moment in time as the Unix timestamp.
    /// Returns UnixStart (1970-01-01) if the input is invalid.
    /// </returns>
    /// <remarks>
    /// The method uses 253402300799 (timestamp for year 9999) as a threshold to distinguish between
    /// seconds and milliseconds format. Values larger than this are treated as milliseconds.
    /// </remarks>
    public static DateTimeOffset FromUnixTimestamp(string? timestamp)
    {
        if (string.IsNullOrEmpty(timestamp) || !long.TryParse(timestamp, out long value))
            return UnixStart;

        // Determine if it's seconds or milliseconds based on the number of digits
        return value > 253402300799
            ? DateTimeOffset.FromUnixTimeMilliseconds(value)
            : DateTimeOffset.FromUnixTimeSeconds(value);
    }
}
