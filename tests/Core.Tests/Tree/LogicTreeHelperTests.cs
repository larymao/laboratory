using Lary.Laboratory.Core.Tree;
using System.Linq;

namespace Lary.Laboratory.Core.Tests.Tree;

public class LogicTreeHelperTests
{
    private readonly ListItem[] _items = [
        new (1, Guid.NewGuid().ToString(), 0),
        new (2, Guid.NewGuid().ToString(), 1),
        new (3, Guid.NewGuid().ToString(), 0),
        new (4, Guid.NewGuid().ToString(), 2),
        new (5, Guid.NewGuid().ToString(), 3),
        new (6, Guid.NewGuid().ToString(), 2),
        new (7, Guid.NewGuid().ToString(), 6)
    ];

    public static TheoryData<int?, TreeInfoValidatorDto[]> BuildAsTreesTheoryData => new()
    {
        { null, [new(1, [4, 7], 4), new(3, [5], 2)] },
        { 0, [new(1, [4, 7], 4), new(3, [5], 2)] },
        { 1, [new(1, [4, 7], 4)] },
        { 2, [new(2, [4, 7], 3)] }
    };

    public static TheoryData<int?, TreeInfoValidatorDto[]> BuildAsSubTreesTheoryData => new()
    {
        // { null, [new(1, [4, 7], 4), new(3, [5], 2)] },
        // { 0, [new(1, [4, 7], 4), new(3, [5], 2)] },
        // { 1, [new(2, [4, 7], 3)] },
        { 2, [new(4, [4], 1), new(6, [7], 2)] }
    };

    [Fact]
    public void LogicTreeHelper_BuildAsTrees_ReturnEmptyTree()
    {
        var rootId = int.MaxValue;

        var trees = _items.BuildAsTrees(x => x.Id, x => x.ParentId, rootId);

        trees.Should().BeEmpty();
    }

    [Theory, MemberData(nameof(BuildAsTreesTheoryData))]
    public void LogicTreeHelper_BuildAsTrees_ReturnCorrectTrees(
        int? rootId, TreeInfoValidatorDto[] expected)   
    {
#if NET48_OR_GREATER || NET6_0_OR_GREATER
        var expectedRootNodeIds = expected.Select(dto => dto.RootNodeId).ToHashSet();
#else
        var expectedRootNodeIds = new HashSet<int>(expected.Select(dto => dto.RootNodeId));
#endif
        Func<TreeNode<ListItem>, TreeInfoValidatorDto> treeDto =
                x => expected.Single(dto => dto.RootNodeId == x.Value.Id);

        var trees = rootId.HasValue
            ? _items.BuildAsTrees(x => x.Id, x => x.ParentId, rootId)
            : _items.BuildAsTrees(x => x.Id, x => x.ParentId);

        trees.Count().Should().Be(expected.Length);
        trees.Select(x => x.Value.Id)
            .Should().OnlyContain(rootNodeId => expectedRootNodeIds.Contains(rootNodeId));
#if NET7_0_OR_GREATER
        trees.Should().OnlyContain(x => x.AllLeaves().Select(node => node.Value.Id).Order().SequenceEqual(treeDto(x).LeafNodeIds));
#else
        trees.Should().OnlyContain(x => x.AllLeaves().Select(node => node.Value.Id).OrderBy(x => x).SequenceEqual(treeDto(x).LeafNodeIds));
#endif
        trees.Should().OnlyContain(x => x.MaxDepth() == treeDto(x).MaxDepth);
    }

    [Fact]
    public void LogicTreeHelper_BuildSubTrees_ReturnEmptyTree()
    {
        var rootId = int.MaxValue;

        var trees = _items.BuildSubTrees(x => x.Id, x => x.ParentId, rootId);

        trees.Should().BeEmpty();
    }

    [Theory, MemberData(nameof(BuildAsSubTreesTheoryData))]
    public void LogicTreeHelper_BuildSubTrees_ReturnCorrectTrees(
        int? rootId, TreeInfoValidatorDto[] expected)
    {
#if NET48_OR_GREATER || NET6_0_OR_GREATER
        var expectedRootNodeIds = expected.Select(dto => dto.RootNodeId).ToHashSet();
#else
        var expectedRootNodeIds = new HashSet<int>(expected.Select(dto => dto.RootNodeId));
#endif
        Func<TreeNode<ListItem>, TreeInfoValidatorDto> treeDto =
                x => expected.Single(dto => dto.RootNodeId == x.Value.Id);

        var trees = rootId.HasValue
            ? _items.BuildSubTrees(x => x.Id, x => x.ParentId, rootId)
            : _items.BuildSubTrees(x => x.Id, x => x.ParentId);

        trees.Count().Should().Be(expected.Length);
        trees.Select(x => x.Value.Id)
            .Should().OnlyContain(rootNodeId => expectedRootNodeIds.Contains(rootNodeId));
#if NET7_0_OR_GREATER
        trees.Should().OnlyContain(x => x.AllLeaves().Select(node => node.Value.Id).Order().SequenceEqual(treeDto(x).LeafNodeIds));
#else
        trees.Should().OnlyContain(x => x.AllLeaves().Select(node => node.Value.Id).OrderBy(x => x).SequenceEqual(treeDto(x).LeafNodeIds));
#endif
        trees.Should().OnlyContain(x => x.MaxDepth() == treeDto(x).MaxDepth);
    }

#if NET7_0_OR_GREATER
    internal record ListItem(int Id, string? Desc, int ParentId);

    public record TreeInfoValidatorDto(int RootNodeId, int[] LeafNodeIds, int MaxDepth);
#else
    internal class ListItem(int id, string? desc, int parentId)
    {
        public int Id { get; } = id;
        public string? Desc { get; } = desc;
        public int ParentId { get; } = parentId;
    }

    public class TreeInfoValidatorDto(int rootNodeId, int[] leafNodeIds, int maxDepth)
    {
        public int RootNodeId { get; } = rootNodeId;
        public int[] LeafNodeIds { get; } = leafNodeIds;
        public int MaxDepth { get; } = maxDepth;
    }
#endif
}