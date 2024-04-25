using System.Linq;
using System.Text.RegularExpressions;

namespace Lary.Laboratory.Core;

/// <summary>
/// Provides methods for <see cref="string"/>
/// </summary>
public static class StringHelper
{
    /// <summary>
    /// Regards the first non-empty line as the base line, clears the indents of each line, and returns
    /// the result of <see cref="string.Trim()"/>.
    /// </summary>
    /// <param name="s">The string to be processed.</param>
    /// <returns>A new string that is processed.</returns>
    public static string TrimIndents(this string s)
    {
        var spaceCount = s.Split('\n').First(x => !string.IsNullOrWhiteSpace(x)).TakeWhile(char.IsWhiteSpace).Count();
        return Regex.Replace(s, $"(\r?\n)(?: {{{spaceCount}}})", "$1").Trim();
    }
}
