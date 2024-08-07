using Lary.Laboratory.Core.Tree;

namespace Lary.Laboratory.EPPlusWrapper.Tests;

public class ExcelHelperTests
{
    private static readonly TreeNode<ExcelProperty?> _fooTree = typeof(Foo).BuildAsExcelPropertyTree();
    private static readonly int _headerRows = _fooTree.MaxDepth() - 1;
    private static readonly Foo[] _data2Export = [
        new(1, new("Adam", "test data 1", false, DateTime.Now.AddDays(-1), new(1, "inner test data 1"))),
        new(2, new("Bob", "test data 2", true, DateTime.Now.AddDays(-2), new(2, "inner test data 2"))),
        new(3, new("Cindy", "test data 3", true, DateTime.Now.AddDays(-3), new(3, "inner test data 3"))),
        new(4, new("Douglas", "test data 4", CreateTime: DateTime.Now.AddDays(-4))),
        new(5)
    ];

    public static TheoryData<int, int, string> ExportCellValueTheoryData => new()
    {
        { 1, 1, nameof(Foo.Id) },
        { 2, 1, nameof(Foo.Id) },
        { 1, 2, nameof(Foo.InnerFoo) },
        { 1, 7, nameof(Foo.InnerFoo) },
        { 3, 7, GetExcelPropertyName<DeepInnerFoo>(nameof(DeepInnerFoo.InnerDescription)) },
        { _headerRows + 1, 2, _data2Export[0].InnerFoo!.Name },
        { _headerRows + 2, 4, _data2Export[1].InnerFoo!.IsEnabled.ToString() },
        { _headerRows + 5, 2, string.Empty }
    };

    [Fact]
    public void ExcelHelper_Export_ReturnDefaultSheet()
    {
        using var excel = ExcelHelper.Export(_data2Export, orientation: Orientation.Horizontal);
        var worksheets = excel.Workbook.Worksheets;

        worksheets.Count.Should().Be(1);
        worksheets.Single().Name.Should().Be("Sheet1");
    }

    [Fact]
    public void ExcelHelper_Export_InvalidSheetName()
    {
        FluentActions.Invoking(() => ExcelHelper.Export(_data2Export, string.Empty, orientation: Orientation.Horizontal))
            .Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData("Sheet1", "Sheet1")]
    [InlineData("data", "data")]
    public void ExcelHelper_Export_ReturnCorrectSheetName(string sheetName, string expected)
    {
        using var excel = ExcelHelper.Export(_data2Export, sheetName, orientation: Orientation.Horizontal);
        var worksheets = excel.Workbook.Worksheets;

        worksheets.Count.Should().Be(1);
        worksheets.Single().Name.Should().Be(expected);
    }

    [Fact]
    public void ExcelHelper_Export_ReturnCorrectCellRange()
    {
        var expectedRows = _headerRows + _data2Export.Length;
        var expectedColumns = _fooTree.AllLeaves()
            .Where(l => l.Value!.PropertyInfo!.GetCustomAttribute<ExcelIgnoreAttribute>() == null)
            .Count();

        using var excel = ExcelHelper.Export(_data2Export, orientation: Orientation.Horizontal);
        var worksheet = excel.Workbook.Worksheets["Sheet1"];

        worksheet.Rows.Count().Should().Be(expectedRows);
        worksheet.Columns.Count().Should().Be(expectedColumns);
    }

    [Theory, MemberData(nameof(ExportCellValueTheoryData))]
    public void ExcelHelper_Export_ReturnHorizontalAddressedCellValue(
        int row, int column, string expected)
    {
        using var excel = ExcelHelper.Export(_data2Export, orientation: Orientation.Horizontal);
        var worksheet = excel.Workbook.Worksheets["Sheet1"];
        var cell = worksheet.Cells[GetExcelAddress(row, column)];

        cell.Value.Should().Be(expected);
    }

    [Theory, MemberData(nameof(ExportCellValueTheoryData))]
    public void ExcelHelper_Export_ReturnVerticalAddressedCellValue(
        int column, int row, string expected)
    {
        using var excel = ExcelHelper.Export(_data2Export, orientation: Orientation.Vertical);
        var worksheet = excel.Workbook.Worksheets["Sheet1"];
        var cell = worksheet.Cells[GetExcelAddress(row, column)];

        cell.Value.Should().Be(expected);
    }

    private static string GetExcelPropertyName<T>(string property)
    {
        return typeof(T)
            .GetProperty(property)!
            .GetCustomAttribute<ExcelPropertyAttribute>()!
            .PropertyName;
    }

    private static string GetExcelAddress(int row, int column)
    {
        return $"{ExcelCellBase.GetAddressCol(column)}{row}";
    }

    internal record Foo(
        int Id,
        InnerFoo? InnerFoo = null,
        [property: ExcelIgnore, ExcelProperty("won't output")] string? Ignore = null);

    internal record InnerFoo(
        [property: ExcelProperty("say my name")] string Name,
        [property: ExcelProperty("tell me something")] string? Description = null,
        [property: ExcelProperty("is enabled")] bool IsEnabled = false,
        [property: ExcelProperty("create time")] DateTime CreateTime = default,
        [property: ExcelProperty("inner stuff")] DeepInnerFoo? DeepInnerFoo = null);

    internal record DeepInnerFoo(
        int InnerId,
        [property: ExcelProperty("actual description")] string? InnerDescription = null,
        [property: ExcelProperty("inner byte")] byte InnerByte = default,
        [property: ExcelProperty("inner datetimeoffset")] DateTimeOffset InnerDateTimeOffset = default,
        [property: ExcelProperty("inner decimal")] decimal InnerDecimal = default,
        [property: ExcelProperty("inner double")] double InnerDouble = default,
        [property: ExcelProperty("inner int16")] short InnerInt16 = default,
        [property: ExcelProperty("inner int64")] long InnerInt64 = default,
        [property: ExcelProperty("inner sbyte")] sbyte InnerSByte = default,
        [property: ExcelProperty("inner single")] float InnerSingle = default,
        [property: ExcelProperty("inner uint16")] ushort InnerUInt16 = default,
        [property: ExcelProperty("inner uint32")] uint InnerUInt32 = default,
        [property: ExcelProperty("inner unint64")] ulong InnerUInt64 = default);

    internal record CellValidatorDto(int XCoordinate, int YCoordinate, bool IsMerged, string Value);
}
