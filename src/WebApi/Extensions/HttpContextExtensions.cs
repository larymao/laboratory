using System.Text;

namespace Lary.Laboratory.WebApi.Extensions;

/// <summary>
/// Provides extension methods for <see cref="HttpRequest"/>.
/// </summary>
public static class HttpContextExtensions
{
    /// <summary>
    /// Reads all characters within length limit from the request stream asynchronously and returns them
    /// as one string.
    /// </summary>
    /// <param name="context">A <see cref="HttpContext"/> instance.</param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <returns>A string with all characters read from the request stream.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if count is negative</exception>
    public static async Task<string> ReadRequestContentAsync(this HttpContext context, int count = 8 * 1024)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(count);

        var request = context.Request;

        if (!request.ContentLength.HasValue || request.Body.CanSeek)
            return string.Empty;

        var sbRequestContent = new StringBuilder();

        try
        {
            request.Body.Seek(0, SeekOrigin.Begin);

            var bufferLength = 4 * 1024;
            var buffer = new byte[bufferLength];
            int length;

            if (request.ContentLength > count)
            {
                var cachedLength = 0;

                while ((length = await request.Body.ReadAsync(buffer.AsMemory(0, bufferLength))) > 0)
                {
                    length = Math.Min(count - cachedLength, length);

                    sbRequestContent.Append(Encoding.UTF8.GetString(buffer), 0, length);

                    cachedLength += bufferLength;

                    if (cachedLength >= count)
                        break;
                }

                sbRequestContent.Append("...");
            }
            else
            {
                while ((length = await request.Body.ReadAsync(buffer.AsMemory(0, bufferLength))) > 0)
                    sbRequestContent.Append(Encoding.UTF8.GetString(buffer), 0, length);
            }
        }
        finally
        {
            if (request.Body.CanSeek)
                request.Body.Seek(0, SeekOrigin.Begin);
        }

        return sbRequestContent.ToString();
    }
}
