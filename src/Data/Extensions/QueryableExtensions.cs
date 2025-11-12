using Lary.Laboratory.Data.Pagination;

namespace Lary.Laboratory.Data.Extensions;

/// <summary>
/// Extension methods for IQueryable.
/// Note: These extensions are ORM-agnostic and can be used with any LINQ provider.
/// </summary>
public static class QueryableExtensions
{
    /// <summary>
    /// Applies paging to a queryable.
    /// </summary>
    /// <typeparam name="T">The type of entity.</typeparam>
    /// <param name="query">The queryable.</param>
    /// <param name="pageIndex">The page number (1-based).</param>
    /// <param name="pageSize">The page size.</param>
    /// <returns>A queryable with paging applied.</returns>
    public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, int pageIndex, int pageSize)
    {
        return query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
    }

    /// <summary>
    /// Applies paging to a queryable.
    /// </summary>
    /// <typeparam name="T">The type of entity.</typeparam>
    /// <param name="query">The queryable.</param>
    /// <param name="request">The paged request.</param>
    /// <returns>A queryable with paging applied.</returns>
    public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, PagedRequest request)
    {
        return query.ApplyPaging(request.PageIndex, request.PageSize);
    }

    /// <summary>
    /// Converts a queryable to a paged list.
    /// </summary>
    /// <typeparam name="T">The type of entity.</typeparam>
    /// <param name="query">The queryable.</param>
    /// <param name="pageIndex">The page number (1-based).</param>
    /// <param name="pageSize">The page size.</param>
    /// <returns>A paged list.</returns>
    public static PagedList<T> ToPagedList<T>(
        this IQueryable<T> query,
        int pageIndex,
        int pageSize)
    {
        return PagedList<T>.Create(query, pageIndex, pageSize);
    }

    /// <summary>
    /// Converts a queryable to a paged list.
    /// </summary>
    /// <typeparam name="T">The type of entity.</typeparam>
    /// <param name="query">The queryable.</param>
    /// <param name="request">The paged request.</param>
    /// <returns>A paged list.</returns>
    public static PagedList<T> ToPagedList<T>(
        this IQueryable<T> query,
        PagedRequest request)
    {
        return query.ToPagedList(request.PageIndex, request.PageSize);
    }
}
