namespace Lary.Laboratory.Data.Pagination;

/// <summary>
/// Implementation of a paged collection of items.
/// </summary>
/// <typeparam name="T">The type of items in the collection.</typeparam>
public class PagedList<T> : IPagedList<T>
{
    /// <inheritdoc />
    public IReadOnlyList<T> Items { get; }

    /// <inheritdoc />
    public int PageIndex { get; }

    /// <inheritdoc />
    public int PageSize { get; }

    /// <inheritdoc />
    public int TotalCount { get; }

    /// <inheritdoc />
    public int TotalPages { get; }

    /// <inheritdoc />
    public bool HasPreviousPage => PageIndex > 1;

    /// <inheritdoc />
    public bool HasNextPage => PageIndex < TotalPages;

    /// <summary>
    /// Initializes a new instance of the <see cref="PagedList{T}"/> class.
    /// </summary>
    /// <param name="items">The items in the current page.</param>
    /// <param name="totalCount">The total number of items.</param>
    /// <param name="pageIndex">The current page number (1-based).</param>
    /// <param name="pageSize">The page size.</param>
    public PagedList(IEnumerable<T> items, int totalCount, int pageIndex, int pageSize)
    {
        Items = items.ToList().AsReadOnly();
        TotalCount = totalCount;
        PageIndex = pageIndex;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
    }

    /// <summary>
    /// Creates a paged list from a queryable source.
    /// </summary>
    /// <param name="source">The queryable source.</param>
    /// <param name="pageIndex">The page number (1-based).</param>
    /// <param name="pageSize">The page size.</param>
    /// <returns>A paged list.</returns>
    public static PagedList<T> Create(
        IQueryable<T> source,
        int pageIndex,
        int pageSize)
    {
        var count = source.Count();
        var items = source
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return new PagedList<T>(items, count, pageIndex, pageSize);
    }

    /// <summary>
    /// Creates an empty paged list.
    /// </summary>
    /// <returns>An empty paged list.</returns>
    public static PagedList<T> Empty() => new PagedList<T>(new List<T>(), 0, 1, 10);
}

