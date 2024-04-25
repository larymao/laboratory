using System;
using System.Collections.Generic;
using System.Linq;

namespace Lary.Laboratory.Core.Tree;

/// <summary>
/// Provides methods for handling logic tree.
/// </summary>
public static class LogicTreeHelper
{
    /// <summary>
    /// Builds items of a given collection as trees.
    /// </summary>
    /// <typeparam name="TNode">Type of items in the collection.</typeparam>
    /// <typeparam name="TId">Type of parent id.</typeparam>
    /// <param name="collection">Collection of items.</param>
    /// <param name="idSelector">Function extracting item's id.</param>
    /// <param name="parentIdSelector">Function extracting item's parent id.</param>
    /// <param name="rootId">Root element id.</param>
    /// <returns>Trees built from items.</returns>
    public static IEnumerable<TreeNode<TNode>> BuildAsTrees<TNode, TId>(
        this IEnumerable<TNode> collection,
        Func<TNode, TId> idSelector,
        Func<TNode, TId> parentIdSelector,
        TId? rootId = default)
    {
        var rootItem = collection.SingleOrDefault(c => idSelector(c) !.Equals(rootId));

        if (rootItem == null)
        {
            return BuildSubTrees(collection, idSelector, parentIdSelector, rootId);
        }

        var rootNode = new TreeNode<TNode>(rootItem);
        rootNode.AddChildren(
            collection.BuildSubTrees(idSelector, parentIdSelector, rootId).ToArray());

        return [rootNode];
    }

    /// <summary>
    /// Builds items of a given collection as trees, excepts root node.
    /// </summary>
    /// <typeparam name="TNode">Type of items in the collection.</typeparam>
    /// <typeparam name="TId">Type of parent id.</typeparam>
    /// <param name="collection">Collection of items.</param>
    /// <param name="idSelector">Function extracting item's id.</param>
    /// <param name="parentIdSelector">Function extracting item's parent id.</param>
    /// <param name="rootId">Root element id.</param>
    /// <returns>Trees built from items.</returns>
    public static IEnumerable<TreeNode<TNode>> BuildSubTrees<TNode, TId>(
        this IEnumerable<TNode> collection,
        Func<TNode, TId> idSelector,
        Func<TNode, TId> parentIdSelector,
        TId? rootId = default)
    {
        var childNodeValues = collection.Where(c => parentIdSelector(c)!.Equals(rootId));

        foreach (var c in childNodeValues)
        {
            var currentNode = new TreeNode<TNode>(c);

            currentNode.AddChildren(
                collection.BuildSubTrees(idSelector, parentIdSelector, idSelector(c)).ToArray());

            yield return currentNode;
        }
    }
}
