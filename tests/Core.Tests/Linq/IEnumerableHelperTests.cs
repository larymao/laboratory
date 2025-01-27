using Lary.Laboratory.Core.Linq;

namespace Lary.Laboratory.Core.Tests.Linq;

public class IEnumerableHelperTests
{
    [Fact]
    public void IEnumerableHelper_OrderByNatural_ReturnCaseSensitiveSortingResult()
    {
        string[] src = ["a", "a1", "a10", string.Empty, "a2", "a1.1", "a1.10", "a1.2", "a1a.1", "a1a.10", "a1a.2", "b", "b1", "b10", "b100", "b2", "A", "A1", "A10", "A2", "A1.1", "A1.10", "A1.2", "A1A.1", "A1A.10", "A1A.2", "B", "B1", "B10", "B100", "B2"];
        string[] expected = [string.Empty, "A", "A1", "A1.1", "A1.2", "A1.10", "A1A.1", "A1A.2", "A1A.10", "A2", "A10", "B", "B1", "B2", "B10", "B100", "a", "a1", "a1.1", "a1.2", "a1.10", "a1a.1", "a1a.2", "a1a.10", "a2", "a10", "b", "b1", "b2", "b10", "b100"];

        var result = src.OrderByNatural(x => x);

        result.SequenceEqual(expected).Should().BeTrue();
    }

    [Fact]
    public void IEnumerableHelper_OrderByNatural_ReturnCaseInsensitiveSortingResult()
    {
        string[] src = ["a", "a1", "a10", string.Empty, "a2", "a1.1", "a1.10", "a1.2", "a1a.1", "a1a.10", "a1a.2", "b", "b1", "b10", "b100", "b2", "A", "A1", "A10", "A2", "A1.1", "A1.10", "A1.2", "A1A.1", "A1A.10", "A1A.2", "B", "B1", "B10", "B100", "B2"];
        string[] expected = [string.Empty, "a", "A", "a1", "A1", "a1.1", "A1.1", "a1.2", "A1.2", "a1.10", "A1.10", "a1a.1", "A1A.1", "a1a.2", "A1A.2", "a1a.10", "A1A.10", "a2", "A2", "a10", "A10", "b", "B", "b1", "B1", "b2", "B2", "b10", "B10", "b100", "B100"];

        var result = src.OrderByNatural(x => x, StringComparer.OrdinalIgnoreCase);

        result.SequenceEqual(expected).Should().BeTrue();
    }

    [Fact]
    public void IEnumerableHelper_OrderByNatural_ReturnCaseSensitiveSortingDescResult()
    {
        string[] src = ["a", "a1", "a10", string.Empty, "a2", "a1.1", "a1.10", "a1.2", "a1a.1", "a1a.10", "a1a.2", "b", "b1", "b10", "b100", "b2", "A", "A1", "A10", "A2", "A1.1", "A1.10", "A1.2", "A1A.1", "A1A.10", "A1A.2", "B", "B1", "B10", "B100", "B2"];
        string[] expected = ["b100", "b10", "b2", "b1", "b", "a10", "a2", "a1a.10", "a1a.2", "a1a.1", "a1.10", "a1.2", "a1.1", "a1", "a", "B100", "B10", "B2", "B1", "B", "A10", "A2", "A1A.10", "A1A.2", "A1A.1", "A1.10", "A1.2", "A1.1", "A1", "A", string.Empty];

        var result = src.OrderByNaturalDescending(x => x);

        result.SequenceEqual(expected).Should().BeTrue();
    }

    [Fact]
    public void IEnumerableHelper_OrderByNatural_ReturnCaseInsensitiveSortingDescResult()
    {
        string[] src = ["a", "a1", "a10", string.Empty, "a2", "a1.1", "a1.10", "a1.2", "a1a.1", "a1a.10", "a1a.2", "b", "b1", "b10", "b100", "b2", "A", "A1", "A10", "A2", "A1.1", "A1.10", "A1.2", "A1A.1", "A1A.10", "A1A.2", "B", "B1", "B10", "B100", "B2"];
        string[] expected = ["b100", "B100", "b10", "B10", "b2", "B2", "b1", "B1", "b", "B", "a10", "A10", "a2", "A2", "a1a.10", "A1A.10", "a1a.2", "A1A.2", "a1a.1", "A1A.1", "a1.10", "A1.10", "a1.2", "A1.2", "a1.1", "A1.1", "a1", "A1", "a", "A", string.Empty];

        var result = src.OrderByNaturalDescending(x => x, StringComparer.OrdinalIgnoreCase);

        result.SequenceEqual(expected).Should().BeTrue();
    }

    [Fact]
    public void IEnumerableHelper_Paging_ShouldWork()
    {
        var src = Enumerable.Range(0, 16).ToArray();
#if NET6_0_OR_GREATER
        var expected = src[^6..];
#else
        var expected = src.Skip(10).Take(6).ToArray();
#endif

        var result = src.Paging(1, 10);

        result.SequenceEqual(expected).Should().BeTrue();
    }

    [Fact]
    public void IEnumerableHelper_WhereIf_ShouldFilter()
    {
        var src = Enumerable.Range(0, 16).ToArray();
        var judgement = true;
#if NET6_0_OR_GREATER
        var expected = src[^6..];
#else
        var expected = src.Skip(10).Take(6).ToArray();
#endif

        var result = src.WhereIf(judgement, i => i >= 10);

        result.SequenceEqual(expected).Should().BeTrue();
    }

    [Fact]
    public void IEnumerableHelper_WhereIf_ShouldSkipFilter()
    {
        var src = Enumerable.Range(0, 16).ToArray();
        var judgement = false;
        var expected = src;

        var result = src.WhereIf(judgement, i => i >= 10);

        result.SequenceEqual(expected).Should().BeTrue();
    }

    [Fact]
    public void IEnumerableHelper_MoveToTop_ByIndex_ShouldWork()
    {
        int[] src = [1, 2, 3, 4, 5];
        int[] expected = [3, 1, 2, 4, 5];

        var result = src.MoveToTopByIndex(2);

        result.SequenceEqual(expected).Should().BeTrue();
    }

    [Fact]
    public void IEnumerableHelper_MoveToTop_ByIndex_ShouldThrowOnNullSource()
    {
        int[] src = null!;

        FluentActions.Invoking(() => src.MoveToTopByIndex(0).ToArray())
            .Should().ThrowExactly<ArgumentNullException>();
    }

    [Fact]
    public void IEnumerableHelper_MoveToTop_ByIndex_ShouldThrowOnInvalidIndex()
    {
        int[] src = [1, 2, 3];

        FluentActions.Invoking(() => src.MoveToTopByIndex(-1).ToArray())
            .Should().ThrowExactly<ArgumentOutOfRangeException>();
        FluentActions.Invoking(() => src.MoveToTopByIndex(3).ToArray())
            .Should().ThrowExactly<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void IEnumerableHelper_MoveToTop_ByIndex_ShouldWorkWithEmptySource()
    {
        var src = Array.Empty<int>();

        FluentActions.Invoking(() => src.MoveToTopByIndex(0).ToArray())
            .Should().ThrowExactly<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void IEnumerableHelper_MoveToTop_ByPredicate_ShouldWork()
    {
        int[] src = [1, 2, 3, 4, 5];
        int[] expected = [4, 5, 1, 2, 3];

        var result = src.MoveToTop(x => x > 3);

        result.SequenceEqual(expected).Should().BeTrue();
    }

    [Fact]
    public void IEnumerableHelper_MoveToTop_ByPredicate_ShouldThrowOnNullSource()
    {
        int[] src = null!;

        FluentActions.Invoking(() => src.MoveToTop(x => x > 0).ToArray())
            .Should().ThrowExactly<ArgumentNullException>();
    }

    [Fact]
    public void IEnumerableHelper_MoveToTop_ByPredicate_ShouldReturnEmptyForEmptySource()
    {
        var src = Array.Empty<int>();
        var expected = Array.Empty<int>();

        var result = src.MoveToTop(x => x > 0);

        result.SequenceEqual(expected).Should().BeTrue();
    }

    [Fact]
    public void IEnumerableHelper_MoveToTop_ByPredicate_ShouldReturnEmptyForNullPredicate()
    {
        int[] src = [1, 2, 3, 4, 5];
        int[] expected = src;

        var result = src.MoveToTop((Func<int, bool>)null!);

        FluentActions.Invoking(() => result.ToArray())
            .Should().ThrowExactly<ArgumentNullException>();
    }

    [Fact]
    public void IEnumerableHelper_MoveToTop_ByItem_ShouldWork()
    {
        int[] src = [1, 2, 3, 4, 5];
        int[] expected = [3, 1, 2, 4, 5];

        var result = src.MoveToTop(3);

        result.SequenceEqual(expected).Should().BeTrue();
    }

    [Fact]
    public void IEnumerableHelper_MoveToTop_ByItem_ShouldThrowOnNullSource()
    {
        int[] src = null!;

        FluentActions.Invoking(() => src.MoveToTop(1).ToArray())
            .Should().ThrowExactly<ArgumentNullException>();
    }

    [Fact]
    public void IEnumerableHelper_MoveToTop_ByItem_ShouldReturnOriginalWhenItemNotFound()
    {
        int[] src = [1, 2, 3, 4, 5];
        int[] expected = src;

        var result = src.MoveToTop(6);

        result.SequenceEqual(expected).Should().BeTrue();
    }

    [Fact]
    public void IEnumerableHelper_MoveToTop_ByItem_ShouldWorkWithNullItem()
    {
        string?[] src = ["a", null, "b", "c"];
        string?[] expected = [null, "a", "b", "c"];
        string? item = null;

        var result = src.MoveToTop(item);

        result.SequenceEqual(expected).Should().BeTrue();
    }
}
