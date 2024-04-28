using Lary.Laboratory.Core.Tree;

namespace Lary.Laboratory.Core.Tests.Tree;

/// <summary>
/// Provides tests for <see cref="LogicTreeHelper"/>.
/// </summary>
[TestClass]
public class LogicTreeHelperTests
{
    private static readonly ListItem[] list =
    [
        new ListItem { Id = 1, Desc = Guid.NewGuid().ToString(), ParentId = 0 },
        new ListItem { Id = 2, Desc = Guid.NewGuid().ToString(), ParentId = 1 },
        new ListItem { Id = 3, Desc = Guid.NewGuid().ToString(), ParentId = 0 },
        new ListItem { Id = 4, Desc = Guid.NewGuid().ToString(), ParentId = 2 },
        new ListItem { Id = 5, Desc = Guid.NewGuid().ToString(), ParentId = 3 },
        new ListItem { Id = 6, Desc = Guid.NewGuid().ToString(), ParentId = 2 },
        new ListItem { Id = 7, Desc = Guid.NewGuid().ToString(), ParentId = 6 }
    ];

    [TestMethod]
    [DataRow(null)]
    [DataRow(0)]
    [DataRow(1)]
    public void BuildListAsTrees(int? rootId)
    {
        var tree = rootId.HasValue
            ? list.BuildAsTrees(x => x.Id, x => x.ParentId, rootId)
            : list.BuildAsTrees(x => x.Id, x => x.ParentId);

        Console.WriteLine(JsonConvert.SerializeObject(tree, Formatting.Indented));
    }

    [TestMethod]
    [DataRow(null)]
    [DataRow(0)]
    [DataRow(1)]
    public void BuildSubTrees(int? rootId)
    {
        var tree = rootId.HasValue
            ? list.BuildSubTrees(x => x.Id, x => x.ParentId, rootId)
            : list.BuildSubTrees(x => x.Id, x => x.ParentId);

        Console.WriteLine(JsonConvert.SerializeObject(tree, Formatting.Indented));
    }

    internal class ListItem
    {
        public int Id { get; set; }

        public string? Desc { get; set; }

        public int ParentId { get; set; }
    }
}
