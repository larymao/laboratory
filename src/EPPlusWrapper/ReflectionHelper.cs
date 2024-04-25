using Lary.Laboratory.Core.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Lary.Laboratory.EPPlusWrapper;

internal static class ReflectionHelper
{
    public static IEnumerable<PropertyInfo> GetValidProperties(this Type type)
    {
        var allProps = type.GetProperties();

        foreach (var prop in allProps)
        {
            if (prop.GetCustomAttributes<ExcelIngoreAttribute>().Any())
            {
                continue;
            }

            yield return prop;
        }
    }

    public static TreeNode<ExcelProperty?> BuildAsExcelPropertyTree(this Type type, object? valueProvider = null)
    {
        var rootNode = new TreeNode<ExcelProperty?>(default);
        rootNode.AddChildren(BuildSubProperties(type, valueProvider));

        var rootNodeLeaves = rootNode.AllLeaves().ToList();
        rootNode.Traverse(node =>
        {
            if (node.Value == null)
            {
                return;
            }

            var firstLeaf = node.AllLeaves().First();
            node.Value.CellIndex = rootNodeLeaves.IndexOf(firstLeaf);
        });

        return rootNode;
    }

    private static TreeNode<ExcelProperty?>[] BuildSubProperties(this Type type, object? valueProvider)
    {
        if (type.IsPrimitive
            || type == typeof(string)
            || type == typeof(DateTime)
            || type == typeof(DateTimeOffset))
        {
            return [];
        }

        var result = new List<TreeNode<ExcelProperty?>>();
        var props = type.GetValidProperties().ToArray();

        for (var i = 0; i < props.Length; i++)
        {
            var prop = props[i];

            var currentNode = new TreeNode<ExcelProperty?>(new ExcelProperty
            {
                ValueProvider = valueProvider,
                PropertyInfo = prop
            });

            var innerValueProvider = valueProvider == null ? null : prop.GetValue(valueProvider);
            var subTree = BuildSubProperties(prop.PropertyType, innerValueProvider);
            currentNode.AddChildren(subTree);
            currentNode.Value!.CellLength = subTree.Length == 0 ? 1 : currentNode.LeafCount();

            result.Add(currentNode);
        }

        return [.. result];
    }
}
