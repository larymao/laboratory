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
        => NaturalSorter.Sort(source, selector, true, StringComparer.Ordinal);

    /// <summary>
    /// Natural sorting in descending order.
    /// </summary>
    /// <typeparam name="T">The type of the elements of source.</typeparam>
    /// <param name="source">A sequence of values to order.</param>
    /// <param name="selector">A function to extract a key from an element.</param>
    /// <returns>An <see cref="IOrderedEnumerable{TElement}"/>> whose elements are sorted according to a key.</returns>
    public static IOrderedEnumerable<T> OrderByNaturalDescending<T>(this IEnumerable<T> source, Func<T, string> selector)
        => NaturalSorter.Sort(source, selector, false, StringComparer.Ordinal);

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
        => NaturalSorter.Sort(source, selector, true, comparer);

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
        => NaturalSorter.Sort(source, selector, false, comparer);

    /// <summary>
    /// Gets a subsequence of the source sequence with a specified pagination config.
    /// </summary>
    /// <typeparam name="T">>The type of the elements of source.</typeparam>
    /// <param name="source">>A sequence.</param>
    /// <param name="pageIndex">Page index, zero based.</param>
    /// <param name="pageSize">Items number per page.</param>
    /// <returns>A subsequence that is taken from the source with the given pagination config.</returns>
    public static IEnumerable<T> Paging<T>(this IEnumerable<T> source, int pageIndex, int pageSize)
        => source.Skip(pageIndex * pageSize).Take(pageSize);

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
        => judgement ? source.Where(predicate) : source;

    /// <summary>
    /// Moves an element at the specified index to the beginning of the sequence.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the sequence.</typeparam>
    /// <param name="source">The source sequence.</param>
    /// <param name="index">The zero-based index of the element to move.</param>
    /// <returns>A new sequence with the specified element moved to the beginning.</returns>
    /// <exception cref="ArgumentNullException">Thrown when source is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when index is less than 0 or greater than or equal to the sequence length.
    /// </exception>
    public static IEnumerable<T> MoveToTopByIndex<T>(this IEnumerable<T> source, int index)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        IList<T> list = source as IList<T> ?? [..source];
        if (index < 0 || index >= list.Count)
            throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");

        yield return list[index];
        for (int i = 0; i < list.Count; i++)
        {
            if (i != index)
                yield return list[i];
        }
    }

    /// <summary>
    /// Moves all elements that match the predicate to the beginning of the sequence.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the sequence.</typeparam>
    /// <param name="source">The source sequence.</param>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <returns>A new sequence with matching elements moved to the beginning.</returns>
    /// <exception cref="ArgumentNullException">Thrown when source is null.</exception>
    public static IEnumerable<T> MoveToTop<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        var lookup = source.GroupBy(predicate).ToLookup(g => g.Key, g => g);

        foreach (var item in lookup[true].SelectMany(x => x))
            yield return item;

        foreach (var item in lookup[false].SelectMany(x => x))
            yield return item;
    }

    /// <summary>
    /// Moves the first occurrence of the specified item to the beginning of the sequence.
    /// If the item is not found in the sequence, returns the original sequence unchanged.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the sequence.</typeparam>
    /// <param name="source">The source sequence.</param>
    /// <param name="item">The item to move to the beginning.</param>
    /// <returns>A new sequence with the specified item moved to the beginning if found; otherwise, the original sequence.</returns>
    /// <exception cref="ArgumentNullException">Thrown when source is null.</exception>
    public static IEnumerable<T> MoveToTop<T>(this IEnumerable<T> source, T item)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        if (!source.Contains(item))
        {
            foreach (var sourceItem in source)
                yield return sourceItem;

            yield break;
        }

        yield return item;

        foreach (var sourceItem in source)
        {
            if (!EqualityComparer<T>.Default.Equals(sourceItem, item))
                yield return sourceItem;
        }
    }
}
