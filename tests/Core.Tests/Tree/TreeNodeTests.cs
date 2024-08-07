using Lary.Laboratory.Core.Tree;

namespace Lary.Laboratory.Core.Tests.Tree;

public class TreeNodeTests
{
    [Fact]
    public void TreeNode_AddChild_ShouldWork()
    {
        var rootNode = new TreeNode<int>(0);
        rootNode.AddChild(1);

        rootNode.Children.Count.Should().Be(1);
    }

    [Fact]
    public void TreeNode_AddChildrenByValue_ShouldWork()
    {
        var rootNode = new TreeNode<int>(0);
        rootNode.AddChildren([1, 2]);

        rootNode.Children.Count.Should().Be(2);
    }

    [Fact]
    public void TreeNode_AddChildren_ShouldWork()
    {
        var rootNode = new TreeNode<int>(0);
        rootNode.AddChildren([new TreeNode<int>(1), new TreeNode<int>(2)]);

        rootNode.Children.Count.Should().Be(2);
    }

    [Fact]
    public void TreeNode_Index_ShouldWork()
    {
        var rootNode = new TreeNode<int>(0);
        rootNode.AddChildren([1, 2]);
        var child1 = rootNode[0];
        var child2 = rootNode[1];

        child1.Value.Should().Be(1);
        child2.Value.Should().Be(2);
    }

    [Fact]
    public void TreeNode_RemoveChild_ShouldWork()
    {
        var rootNode = new TreeNode<int>(0);
        rootNode.AddChildren([1, 2]);

        rootNode.RemoveChild(rootNode[0]);

        rootNode.Children.Count.Should().Be(1);
        rootNode[0].Value.Should().Be(2);
    }
}
