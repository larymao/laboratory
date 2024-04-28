using System.Collections.Generic;

namespace Lary.Laboratory.Core.Linq;

/// <summary>
/// Pagination config.
/// </summary>
public class Pager
{
    /// <summary>
    /// Default pagination index.
    /// </summary>
    public const int DEFAULT_PAGE_INDEX = 0;

    /// <summary>
    /// Items number per page by default.
    /// </summary>
    public const int DEFAULT_PAGE_SIZE = 10;

    private int pageIndex;
    private int pageSize;

    /// <summary>
    /// Index of current page, zero based.
    /// </summary>
    public int PageIndex
    {
        get
        {
            return pageIndex >= 0 ? pageIndex : DEFAULT_PAGE_INDEX;
        }

        set
        {
            pageIndex = value;
        }
    }

    /// <summary>
    /// Items number per page.
    /// </summary>
    public int PageSize
    {
        get
        {
            return pageSize > 0 ? pageSize : DEFAULT_PAGE_SIZE;
        }
        set
        {
            pageSize = value;
        }
    }

    /// <summary>
    /// Sorting rules.
    /// </summary>
    public List<SortRule>? SortRules { get; set; }

    /// <summary>
    /// Creates an instance of <see cref="Pager"/>.
    /// </summary>
    public Pager()
    {
    }

    /// <summary>
    /// Creates an instance of <see cref="Pager"/>.
    /// </summary>
    /// <param name="pageIndex">Index of current page, zero based.</param>
    /// <param name="pageSize">Items number per page.</param>
    public Pager(int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
    }
}
