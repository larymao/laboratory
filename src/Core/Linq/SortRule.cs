namespace Lary.Laboratory.Core.Linq;

/// <summary>
/// Sorting rule.
/// </summary>
public class SortRule
{
    /// <summary>
    /// Sorting field.
    /// </summary>
    public string Field { get; set; } = default!;

    /// <summary>
    /// Sets to <see langword="true"/> to sort ascending; otherwise, false.
    /// </summary>
    public bool Ascending { get; set; }
}
