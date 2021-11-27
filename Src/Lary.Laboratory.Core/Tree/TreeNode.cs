using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Lary.Laboratory.Core.Tree
{
    /// <summary>
    /// Tree node.
    /// </summary>
    /// <typeparam name="T">Type of tree node.</typeparam>
    public class TreeNode<T>
    {
        private readonly T _value;
        private readonly List<TreeNode<T>> _children = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeNode{T}"/> class.
        /// </summary>
        /// <param name="value">The value of current tree node.</param>
        public TreeNode(T value)
        {
            _value = value;
        }

        public TreeNode<T> this[int i]
        {
            get { return _children[i]; }
        }

        /// <summary>
        /// Parent node.
        /// </summary>
        [JsonIgnore]
        public TreeNode<T>? Parent { get; private set; }

        /// <summary>
        /// Value of current node.
        /// </summary>
        public T Value { get { return _value; } }

        /// <summary>
        /// Children.
        /// </summary>
        public ReadOnlyCollection<TreeNode<T>> Children
        {
            get { return _children.AsReadOnly(); }
        }

        /// <summary>
        /// Whether current node is a leaf node.
        /// </summary>
        public bool IsLeaf
        {
            get { return _children == null || _children.Count == 0; }
        }

        /// <summary>
        /// Adds a child node to current node.
        /// </summary>
        /// <param name="value">Value of child node to be added.</param>
        /// <returns>A <see cref="TreeNode{T}"/> instance that represents the child node being added.</returns>
        public TreeNode<T> AddChild(T value)
        {
            var node = new TreeNode<T>(value) { Parent = this };
            _children.Add(node);
            return node;
        }

        /// <summary>
        /// Adds a child node to current node.
        /// </summary>
        /// <param name="child">The child node to be added.</param>
        /// <returns>A <see cref="TreeNode{T}"/> instance that represents the child node being added.</returns>
        public TreeNode<T> AddChild(TreeNode<T> child)
        {
            child.Parent = this;
            _children.Add(child);
            return child;
        }

        /// <summary>
        /// Adds multiple child nodes to current node.
        /// </summary>
        /// <param name="values">Values of child nodes to be added.</param>
        /// <returns>An <see cref="Array"/> whose elements are the child nodes being added.</returns>
        public TreeNode<T>[] AddChildren(params T[] values)
        {
            return values.Select(AddChild).ToArray();
        }

        /// <summary>
        /// Adds multiple child nodes to current node.
        /// </summary>
        /// <param name="children">The child nodes to be added.</param>
        /// <returns>An <see cref="Array"/> whose elements are the child nodes being added.</returns>
        public TreeNode<T>[] AddChildren(params TreeNode<T>[] children)
        {
            return children.Select(AddChild).ToArray();
        }

        /// <summary>
        /// Removes a child node from current node.
        /// </summary>
        /// <param name="node">The child node to be removed.</param>
        /// <returns><see langword="true"/> if current operation succeeded; otherwise, <see langword="false"/>.</returns>
        public bool RemoveChild(TreeNode<T> node)
        {
            return _children.Remove(node);
        }

        public IEnumerable<TreeNode<T>> AllLeaves()
        {
            return Flatten().Where(n => n.IsLeaf);
        }

        /// <summary>
        /// Traverses the sub tree from current tree node.
        /// </summary>
        /// <param name="action">The action to apply to each tree node.</param>
        public void Traverse(Action<TreeNode<T>> action)
        {
            action(this);

            foreach (var child in _children)
            {
                child.Traverse(action);
            }
        }

        /// <summary>
        /// Flattens the sub tree from current tree node.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> whose elements are the values of all leaf nodes.</returns>
        public IEnumerable<TreeNode<T>> Flatten()
        {
            return new[] { this }.Concat(_children.SelectMany(x => x.Flatten()));
        }

        /// <summary>
        /// Calculates the max depth of the sub tree of current node.
        /// </summary>
        /// <returns>The max depth of the sub tree of current node.</returns>
        public int MaxDepth()
        {
            return MaxDepth(this, 1);
        }

        /// <summary>
        /// Calculates the level of the current node of the full tree.
        /// </summary>
        /// <returns>The level of the current node of the full tree.</returns>
        public int CurrentLevel()
        {
            return ParentLevel(this) + 1;
        }

        /// <summary>
        /// Calculates the total count of leaf nodes.
        /// </summary>
        /// <returns>The total count of leaf nodes.</returns>
        public int LeafCount()
        {
            return _children.Sum(subTree =>
                subTree.Flatten().Count(node => node.Children == null || node.Children.Count == 0));
        }

        private int MaxDepth(TreeNode<T> currentNode, int currentDepth)
        {
            var maxDepth = currentDepth;

            foreach (var node in currentNode.Children)
            {
                maxDepth = System.Math.Max(maxDepth, MaxDepth(node, currentDepth + 1));
            }

            return maxDepth;
        }

        private int ParentLevel(TreeNode<T> currentNode)
        {
            if (currentNode.Parent == null)
            {
                return -1;
            }

            return ParentLevel(currentNode.Parent) + 1;
        }
    }
}
