namespace Lary.Laboratory.EPPlusWrapper.Tests;

/// <summary>
/// Provides test methods for <see cref="ExcelHelper"/>
/// </summary>
[TestClass]
public class ExcelHelperTests
{
    /// <summary>
    /// Tests excel operations.
    /// </summary>
    [TestMethod]
    [DataRow(Orientation.Horizontal)]
    // [DataRow(Orientation.Vertical)]
    public async Task ExcelOperationsTest(Orientation orientation)
    {
        var fileName = $"test_foo_{DateTime.Now: yyyyMMddHHmmss}.xlsx";
        var file = File.Create(fileName);

        try
        {
            var data = new List<Foo>
            {
                new() { Id = 1, InnerFoo = new InnerFoo { Name = "Adam", Description = "test data 1", CreateTime = DateTime.Now.AddDays(-1), DeepInnerFoo = new DeepInnerFoo { InnerId = 1, InnerDescription = "inner test data 1" } } },
                new() { Id = 2, InnerFoo = new InnerFoo { Name = "Bob", Description = "test data 2", CreateTime = DateTime.Now.AddDays(-2), DeepInnerFoo = new DeepInnerFoo { InnerId = 2, InnerDescription = "inner test data 2" } } },
                new() { Id = 3, InnerFoo = new InnerFoo { Name = "Cindy", Description = "test data 3", CreateTime = DateTime.Now.AddDays(-3), DeepInnerFoo = new DeepInnerFoo { InnerId = 3, InnerDescription = "inner test data 3" } } },
                new() { Id = 4, InnerFoo = new InnerFoo { Name = "Douglas", Description = "test data 4", CreateTime = DateTime.Now.AddDays(-4) } },
                new() { Id = 5 }
            };

            var excelPackage = ExcelHelper.Export(data, orientation: orientation);

            // beautifies layout
            excelPackage.Workbook.Worksheets["Sheet1"].DataRange().AutoFitColumns();

            await excelPackage.SaveAsAsync(file);
            file.Dispose();

            Debug.Write($"File exported at {Path.GetFullPath(fileName)}");
        }
        finally
        {
            File.Delete(fileName);
        }
    }

    internal class Foo
    {
        public int Id { get; set; }

        public InnerFoo? InnerFoo { get; set; }

        [ExcelIngore]
        [ExcelProperty("忽略")]
        public string? Ignore { get; set; }
    }

    internal class InnerFoo
    {
        [ExcelProperty("名字")]
        public string Name { get; set; } = default!;

        [ExcelProperty("描述")]
        public string? Description { get; set; }

        [ExcelProperty("内部信息")]
        public DeepInnerFoo? DeepInnerFoo { get; set; }

        [ExcelProperty("启用")]
        public bool IsEnabled { get; set; }

        [ExcelProperty("创建时间")]
        public DateTime CreateTime { get; set; }
    }

    internal class DeepInnerFoo
    {
        public int InnerId { get; set; }

        [ExcelProperty("内部描述")]
        public string? InnerDescription { get; set; }
    }
}
