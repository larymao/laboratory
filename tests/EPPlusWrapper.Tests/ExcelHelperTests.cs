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
#if NET7_0_OR_GREATER
        new(4, new("Douglas", "test data 4", CreateTime: DateTime.Now.AddDays(-4))),
#else
        new(4, new("Douglas", "test data 4", createTime: DateTime.Now.AddDays(-4))),
#endif
        new(5)
    ];

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

#if NET7_0_OR_GREATER
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
#else
    internal class Foo(int id, InnerFoo? innerFoo = null, string? ignore = null)
    {
        public int Id { get; } = id;
        public InnerFoo? InnerFoo { get; } = innerFoo;
        [ExcelIgnore, ExcelProperty("won't output")]
        public string? Ignore { get; } = ignore;
    }

    internal class InnerFoo(string name, string? description = null, bool isEnabled = false,
        DateTime createTime = default, DeepInnerFoo? deepInnerFoo = null)
    {
        [ExcelProperty("say my name")]
        public string Name { get; } = name;
        [ExcelProperty("tell me something")]
        public string? Description { get; } = description;
        [ExcelProperty("is enabled")]
        public bool IsEnabled { get; } = isEnabled;
        [ExcelProperty("create time")]
        public DateTime CreateTime { get; } = createTime;
        [ExcelProperty("inner stuff")]
        public DeepInnerFoo? DeepInnerFoo { get; } = deepInnerFoo;
    }

    internal class DeepInnerFoo(int innerId, string? innerDescription = null, byte innerByte = default,
        DateTimeOffset innerDateTimeOffset = default, decimal innerDecimal = default,
        double innerDouble = default, short innerInt16 = default, long innerInt64 = default,
        sbyte innerSByte = default, float innerSingle = default, ushort innerUInt16 = default,
        uint innerUInt32 = default, ulong innerUInt64 = default)
    {
        public int InnerId { get; } = innerId;
        [ExcelProperty("actual description")]
        public string? InnerDescription { get; } = innerDescription;
        [ExcelProperty("inner byte")]
        public byte InnerByte { get; } = innerByte;
        [ExcelProperty("inner datetimeoffset")]
        public DateTimeOffset InnerDateTimeOffset { get; } = innerDateTimeOffset;
        [ExcelProperty("inner decimal")]
        public decimal InnerDecimal { get; } = innerDecimal;
        [ExcelProperty("inner double")]
        public double InnerDouble { get; } = innerDouble;
        [ExcelProperty("inner int16")]
        public short InnerInt16 { get; } = innerInt16;
        [ExcelProperty("inner int64")]
        public long InnerInt64 { get; } = innerInt64;
        [ExcelProperty("inner sbyte")]
        public sbyte InnerSByte { get; } = innerSByte;
        [ExcelProperty("inner single")]
        public float InnerSingle { get; } = innerSingle;
        [ExcelProperty("inner uint16")]
        public ushort InnerUInt16 { get; } = innerUInt16;
        [ExcelProperty("inner uint32")]
        public uint InnerUInt32 { get; } = innerUInt32;
        [ExcelProperty("inner unint64")]
        public ulong InnerUInt64 { get; } = innerUInt64;
    }

    internal class CellValidatorDto(int xCoordinate, int yCoordinate, bool isMerged, string value)
    {
        public int XCoordinate { get; } = xCoordinate;
        public int YCoordinate { get; } = yCoordinate;
        public bool IsMerged { get; } = isMerged;
        public string Value { get; } = value;
    }
#endif
}
