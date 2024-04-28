using Lary.Laboratory.Core.Utils;

namespace Lary.Laboratory.Core.Linq;

/// <summary>
/// Provides methods for <see cref="IOrderedEnumerable{TElement}"/>
/// </summary>
public static class IOrderedEnumerableHelper
{
    /// <summary>
    /// Performs a subsequent natural ordering of the elements in a sequence in ascending order
    /// by using a specified comparer.
    /// </summary>
    /// <typeparam name="T">The type of the elements of source.</typeparam>
    /// <param name="source">A sequence of values to order.</param>
    /// <param name="selector">A function to extract a key from an element.</param>
    /// <returns>An <see cref="IOrderedEnumerable{TElement}"/>> whose elements are sorted according to a key.</returns>
    public static IOrderedEnumerable<T> ThenByNatural<T>(
        this IOrderedEnumerable<T> source, Func<T, string> selector)
    {
        return NaturalSorter.SubSort(source, selector, true, StringComparer.Ordinal);
    }

    /// <summary>
    /// Performs a subsequent natural ordering of the elements in a sequence in descending order
    /// by using a specified comparer.
    /// </summary>
    /// <typeparam name="T">The type of the elements of source.</typeparam>
    /// <param name="source">A sequence of values to order.</param>
    /// <param name="selector">A function to extract a key from an element.</param>
    /// <returns>An <see cref="IOrderedEnumerable{TElement}"/>> whose elements are sorted according to a key.</returns>
    public static IOrderedEnumerable<T> ThenByNaturalDescending<T>(
        this IOrderedEnumerable<T> source, Func<T, string> selector)
    {
        return NaturalSorter.SubSort(source, selector, false, StringComparer.Ordinal);
    }

    /// <summary>
    /// Performs a subsequent natural ordering of the elements in a sequence in ascending order
    /// by using a specified comparer.
    /// </summary>
    /// <typeparam name="T">The type of the elements of source.</typeparam>
    /// <param name="source">A sequence of values to order.</param>
    /// <param name="selector">A function to extract a key from an element.</param>
    /// <param name="comparer">A <see cref="StringComparer"/> to compare keys.</param>
    /// <returns>An <see cref="IOrderedEnumerable{TElement}"/>> whose elements are sorted according to a key.</returns>
    public static IOrderedEnumerable<T> ThenByNatural<T>(
        this IOrderedEnumerable<T> source, Func<T, string> selector, StringComparer comparer)
    {
        return NaturalSorter.SubSort(source, selector, true, comparer);
    }

    /// <summary>
    /// Performs a subsequent natural ordering of the elements in a sequence in descending order
    /// by using a specified comparer.
    /// </summary>
    /// <typeparam name="T">The type of the elements of source.</typeparam>
    /// <param name="source">A sequence of values to order.</param>
    /// <param name="selector">A function to extract a key from an element.</param>
    /// <param name="comparer">A <see cref="StringComparer"/> to compare keys.</param>
    /// <returns>An <see cref="IOrderedEnumerable{TElement}"/>> whose elements are sorted according to a key.</returns>
    public static IOrderedEnumerable<T> ThenByNaturalDescending<T>(
        this IOrderedEnumerable<T> source, Func<T, string> selector, StringComparer comparer)
    {
        return NaturalSorter.SubSort(source, selector, false, comparer);
    }
}
