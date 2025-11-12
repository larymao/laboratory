namespace Lary.Laboratory.Data.Pagination;

/// <summary>
/// Represents a paged request with page index and page size.
/// </summary>
public class PagedRequest
{
    private int _pageIndex = 1;
    private int _pageSize = 10;

    /// <summary>
    /// Gets or sets the page number (1-based). Default is 1.
    /// </summary>
    public int PageIndex
    {
        get => _pageIndex;
        set => _pageIndex = value < 1 ? 1 : value;
    }

    /// <summary>
    /// Gets or sets the page size. Default is 10. Maximum is 100.
    /// </summary>
    public int PageSize
    {
        get => _pageSize;
        set
        {
            if (value < 1)
                _pageSize = 10;
            else if (value > 100)
                _pageSize = 100;
            else
                _pageSize = value;
        }
    }

    /// <summary>
    /// Gets the number of items to skip.
    /// </summary>
    public int Skip => (PageIndex - 1) * PageSize;

    /// <summary>
    /// Gets the number of items to take.
    /// </summary>
    public int Take => PageSize;

    /// <summary>
    /// Initializes a new instance of the <see cref="PagedRequest"/> class.
    /// </summary>
    public PagedRequest()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PagedRequest"/> class.
    /// </summary>
    /// <param name="pageIndex">The page number (1-based).</param>
    /// <param name="pageSize">The page size.</param>
    public PagedRequest(int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
    }
}

