using OfficeOpenXml;

namespace Lary.Laboratory.EPPlusWrapper
{
    /// <summary>
    /// Provides methods for <see cref="ExcelWorksheet"/>.
    /// </summary>
    public static class WorksheetHelper
    {
        /// <summary>
        /// Provides access to a range of cells from the top left cell to the bottom right cell.
        /// </summary>
        /// <param name="worksheet">The <see cref="ExcelWorksheet"/> to access.</param>
        /// <returns>A range of cells from the top left cell to the bottom right cell of the given worksheet.</returns>
        public static ExcelRange DataRange(this ExcelWorksheet worksheet)
        {
            return worksheet.Cells[worksheet.Dimension.Start.Row, worksheet.Dimension.Start.Column, worksheet.Dimension.End.Row, worksheet.Dimension.End.Column];
        }
    }
}
