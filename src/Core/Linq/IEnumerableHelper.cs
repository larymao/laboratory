using Lary.Laboratory.Core.Utils;

namespace Lary.Laboratory.Core.Linq;

/// <summary>
/// Provides methods for <see cref="IEnumerable{T}"/>
/// </summary>
public static class IEnumerableHelper
{
    /// <summary>
    /// Natural sorting in ascending order.
    /// </summary>
    /// <typeparam name="T">The type of the elements of source.</typeparam>
    /// <param name="source">A sequence of values to order.</param>
    /// <param name="selector">A function to extract a key from an element.</param>
    /// <returns>An <see cref="IOrderedEnumerable{TElement}"/>> whose elements are sorted according to a key.</returns>
    public static IOrderedEnumerable<T> OrderByNatural<T>(this IEnumerable<T> source, Func<T, string> selector)
    {
        return NaturalSorter.Sort(source, selector, true, StringComparer.Ordinal);
    }

    /// <summary>
    /// Natural sorting in descending order.
    /// </summary>
    /// <typeparam name="T">The type of the elements of source.</typeparam>
    /// <param name="source">A sequence of values to order.</param>
    /// <param name="selector">A function to extract a key from an element.</param>
    /// <returns>An <see cref="IOrderedEnumerable{TElement}"/>> whose elements are sorted according to a key.</returns>
    public static IOrderedEnumerable<T> OrderByNaturalDescending<T>(this IEnumerable<T> source, Func<T, string> selector)
    {
        return NaturalSorter.Sort(source, selector, false, StringComparer.Ordinal);
    }

    /// <summary>
    /// Natural sorting in ascending order.
    /// </summary>
    /// <typeparam name="T">The type of the elements of source.</typeparam>
    /// <param name="source">A sequence of values to order.</param>
    /// <param name="selector">A function to extract a key from an element.</param>
    /// <param name="comparer">A <see cref="StringComparer"/> to compare keys.</param>
    /// <returns>An <see cref="IOrderedEnumerable{TElement}"/>> whose elements are sorted according to a key.</returns>
    public static IOrderedEnumerable<T> OrderByNatural<T>(
        this IEnumerable<T> source, Func<T, string> selector, StringComparer comparer)
    {
        return NaturalSorter.Sort(source, selector, true, comparer);
    }

    /// <summary>
    /// Natural sorting in descending order.
    /// </summary>
    /// <typeparam name="T">The type of the elements of source.</typeparam>
    /// <param name="source">A sequence of values to order.</param>
    /// <param name="selector">A function to extract a key from an element.</param>
    /// <param name="comparer">A <see cref="StringComparer"/> to compare keys.</param>
    /// <returns>An <see cref="IOrderedEnumerable{TElement}"/>> whose elements are sorted according to a key.</returns>
    public static IOrderedEnumerable<T> OrderByNaturalDescending<T>(
        this IEnumerable<T> source, Func<T, string> selector, StringComparer comparer)
    {
        return NaturalSorter.Sort(source, selector, false, comparer);
    }

    /// <summary>
    /// Gets a subsequence of the source sequence with a specified pagination config.
    /// </summary>
    /// <typeparam name="T">>The type of the elements of source.</typeparam>
    /// <param name="source">>A sequence.</param>
    /// <param name="pageIndex">Page index, zero based.</param>
    /// <param name="pageSize">Items number per page.</param>
    /// <returns>A subsequence that is taken from the source with the given pagination config.</returns>
    public static IEnumerable<T> Page<T>(this IEnumerable<T> source, int pageIndex, int pageSize)
    {
        return source.Page(new Pager(pageIndex, pageSize));
    }

    /// <summary>
    /// Gets a subsequence of the source sequence with a specified pagination config.
    /// </summary>
    /// <typeparam name="T">>The type of the elements of source.</typeparam>
    /// <param name="source">>A sequence.</param>
    /// <param name="pager">Pagination config.</param>
    /// <returns>A subsequence that is taken from the source with the given pagination config.</returns>
    public static IEnumerable<T> Page<T>(this IEnumerable<T> source, Pager pager)
    {
        return source
            .Skip(pager.PageIndex * pager.PageSize)
            .Take(pager.PageSize);
    }

    /// <summary>
    /// Filters a sequence of values based on a predicate once the judgement passed; otherwise, skips.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <param name="source">An <see cref="IEnumerable{T}"/> to filter.</param>
    /// <param name="judgement">If <see langword="true"/>, execute the predicate; otherwise, do nothing.</param>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <returns>
    /// An <see cref="IEnumerable{T}"/> that contains elements from the input sequence that satisfy the condition.
    /// </returns>
    public static IEnumerable<TSource> WhereIf<TSource>(
        this IEnumerable<TSource> source, bool judgement, Func<TSource, bool> predicate)
    {
        if (!judgement)
        {
            return source;
        }

        return source.Where(predicate);
    }
}
