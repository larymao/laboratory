namespace Lary.Laboratory.Core.Json;

/// <summary>
/// Provides methodes for json formatting.
/// </summary>
public static class JsonFormatter
{
    /// <summary>
    /// Formats json string to be a well formatted one.
    /// </summary>
    /// <param name="json">The json string to be formatted.</param>
    /// <param name="oneline">Whether to join json string into one line.</param>
    /// <param name="indent">
    /// The indent of formatted json string. This param won't work if <paramref name="oneline"/> is set to
    /// <see langword="true"/>.
    /// </param>
    /// <returns>A well formatted new json string that is equivalent to the old one.</returns>
    public static string FormatJson(string? json, bool oneline = false, string indent = "    ")
    {
        var indentation = 0;
        var quoteCount = 0;
        var escapeCount = 0;

        var result =
            from ch in json ?? string.Empty
            let escaped = (ch == '\\' ? escapeCount++ : escapeCount > 0 ? escapeCount-- : escapeCount) > 0
            let quotes = ch == '"' && !escaped ? quoteCount++ : quoteCount
            let unquoted = quotes % 2 == 0
            let colon = ch == ':' && unquoted ? ": " : null
            let nospace = char.IsWhiteSpace(ch) && unquoted ? string.Empty : null
            let lineBreak = ch == ',' && unquoted ? (oneline ? ch + " " : ch + Environment.NewLine + string.Concat(Enumerable.Repeat(indent, indentation))) : null
            let openChar = (ch == '{' || ch == '[') && unquoted ? (oneline ? ch + " " : ch + Environment.NewLine + string.Concat(Enumerable.Repeat(indent, ++indentation))) : ch.ToString()
            let closeChar = (ch == '}' || ch == ']') && unquoted ? (oneline ? " " + ch : Environment.NewLine + string.Concat(Enumerable.Repeat(indent, --indentation)) + ch) : ch.ToString()
            select colon ?? nospace ?? lineBreak ?? (openChar.Length > 1 ? openChar : closeChar);

        return string.Concat(result);
    }
}
