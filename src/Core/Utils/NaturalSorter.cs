using System.Text.RegularExpressions;

namespace Lary.Laboratory.Core.Utils;

internal static class NaturalSorter
{
    private static readonly Regex _regex = new(@"\d+", RegexOptions.Compiled);

    public static IOrderedEnumerable<T> Sort<T>(
        IEnumerable<T> source, Func<T, string> selector, bool ascending, StringComparer comparer)
    {
        var maxPaddingWidth = MaxPaddingWidth(source, selector);
        string keySelector(T x) => PaddedKey(x, selector, maxPaddingWidth);

        return ascending
            ? source.OrderBy(keySelector, comparer)
            : source.OrderByDescending(keySelector, comparer);
    }

    public static IOrderedEnumerable<T> SubSort<T>(
        IOrderedEnumerable<T> source, Func<T, string> selector, bool ascending, StringComparer comparer)
    {
        var maxPaddingWidth = MaxPaddingWidth(source, selector);
        string keySelector(T x) => PaddedKey(x, selector, maxPaddingWidth);

        return ascending
            ? source.ThenBy(keySelector, comparer)
            : source.ThenByDescending(keySelector, comparer);
    }

    private static int MaxPaddingWidth<T>(IEnumerable<T> source, Func<T, string> selector)
    {
        return source
            .SelectMany(x => _regex.Matches(selector(x)).Cast<Match>().Select(m => (int?)m.Value.Length))
            .Max() ?? 0;
    }

    private static string PaddedKey<T>(T item, Func<T, string> selector, int maxPaddingWidth)
    {
        return _regex.Replace(selector(item), m => m.Value.PadLeft(maxPaddingWidth, '0'));
    }
}
