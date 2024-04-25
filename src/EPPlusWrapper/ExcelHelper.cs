using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Lary.Laboratory.EPPlusWrapper;

/// <summary>
/// Provides methods to convenient the using of excel.
/// </summary>
public static class ExcelHelper
{
    private const string DefaultSheetName = "Sheet1";

    static ExcelHelper()
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
    }

    /// <summary>
    /// Exports the given data to an excel worksheet of a blank workbook.
    /// </summary>
    /// <param name="data">The data to be exported.</param>
    /// <param name="sheetName">Name of the excel worksheet.</param>
    /// <param name="orientation">The orientation of data records.</param>
    public static ExcelPackage Export<TItem>(
        IEnumerable<TItem> data,
        string sheetName = DefaultSheetName, Orientation orientation = Orientation.Horizontal)
    {
        // creates a blank workbook and inits worksheet
        var excelPackage = new ExcelPackage();
        var worksheet = excelPackage.Workbook.Worksheets.Add(sheetName);

        WriteHeaders(worksheet, typeof(TItem), orientation);
        WriteData(worksheet, data, orientation);

        return excelPackage;
    }

    private static void WriteHeaders(ExcelWorksheet worksheet, Type itemType, Orientation orientation)
    {
        var propertyTree = itemType.BuildAsExcelPropertyTree();
        var headerDepth = propertyTree.MaxDepth() - 1;

        if (orientation == Orientation.Horizontal)
        {
            worksheet.InsertRow(1, headerDepth);
        }
        else
        {
            worksheet.InsertColumn(1, headerDepth);
        }

        propertyTree.Traverse(node =>
        {
            if (node.Parent == null)
            {
                return;
            }

            var levelIndex = node.CurrentLevel();
            var depthIndex = headerDepth - node.MaxDepth() + 1;
            var excelProperty = node.Value!.PropertyInfo!
                .GetCustomAttributes<ExcelPropertyAttribute>()
                .SingleOrDefault();

            var mergedCell = orientation == Orientation.Horizontal
                ? worksheet.Cells[levelIndex, node.Value.CellIndex + 1, depthIndex, node.Value.CellIndex + node.Value.CellLength]
                : worksheet.Cells[node.Value.CellIndex + 1, levelIndex, node.Value.CellIndex + node.Value.CellLength, depthIndex];

            mergedCell.Merge = true;
            mergedCell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            mergedCell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            mergedCell.Value = excelProperty == null ? node.Value!.PropertyInfo!.Name : excelProperty.PropertyName;
        });
    }

    private static void WriteData<TItem>(ExcelWorksheet worksheet, IEnumerable<TItem> data, Orientation orientation)
    {
        foreach (var item in data)
        {
            int orientationIndex;

            if (orientation == Orientation.Horizontal)
            {
                orientationIndex = worksheet.Dimension.End.Row + 1;
                worksheet.InsertRow(orientationIndex, 1);
            }
            else
            {
                orientationIndex = worksheet.Dimension.End.Column + 1;
                worksheet.InsertColumn(orientationIndex, 1);
            }

            var propertyTree = typeof(TItem).BuildAsExcelPropertyTree(item);
            var allLeaves = propertyTree.AllLeaves();

            foreach (var leaf in allLeaves)
            {
                var currentCell = orientation == Orientation.Horizontal
                    ? worksheet.Cells[orientationIndex, leaf.Value!.CellIndex + 1]
                    : worksheet.Cells[leaf.Value!.CellIndex + 1, orientationIndex];

                currentCell.Value = BuildExportCellValue(leaf.Value.GetValue(), leaf.Value.PropertyInfo!.PropertyType);
            }
        }
    }

    private static object? BuildExportCellValue(object? data, Type type)
    {
        if (data == null)
        {
            return string.Empty;
        }

        return type.Name.ToLower() switch
        {
            "byte" => Convert.ToByte(data),
            "datetime" => Convert.ToDateTime(data).ToString("O"),
            "datetimeoffset" => (data as DateTimeOffset?)?.ToString("O"),
            "decimal" => Convert.ToDecimal(data),
            "double" => Convert.ToDouble(data),
            "int16" => Convert.ToInt16(data),
            "int32" => Convert.ToInt32(data),
            "int64" => Convert.ToInt64(data),
            "sbyte" => Convert.ToSByte(data),
            "single" => Convert.ToSingle(data),
            "uint16" => Convert.ToUInt16(data),
            "uint32" => Convert.ToUInt32(data),
            "uint64" => Convert.ToUInt64(data),
            _ => data.ToString()
        };
    }
}
