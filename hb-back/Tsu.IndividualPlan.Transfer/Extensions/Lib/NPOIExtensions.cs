using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;

namespace Tsu.IndividualPlan.Transfer.Extensions.Lib;

public static class NPOIExtensions
{
    public static IRow GetRowSafe(this ISheet sheet, int row)
    {
        return sheet.GetRow(row) ?? sheet.CreateRow(row);
    }

    public static ICell GetCellSafe(this IRow row, int col)
    {
        return row.GetCell(col) ?? row.CreateCell(col);
    }

    public static XSSFCellStyle toXSSF(this HSSFCellStyle hssfStyle, IWorkbook xssfWorkbook)
    {
        var xssfStyle = (XSSFCellStyle)xssfWorkbook.CreateCellStyle();

        xssfStyle.BorderBottom = hssfStyle.BorderBottom;
        xssfStyle.BorderTop = hssfStyle.BorderTop;
        xssfStyle.BorderLeft = hssfStyle.BorderLeft;
        xssfStyle.BorderRight = hssfStyle.BorderRight;
        xssfStyle.BottomBorderColor = hssfStyle.BottomBorderColor;
        xssfStyle.TopBorderColor = hssfStyle.TopBorderColor;
        xssfStyle.LeftBorderColor = hssfStyle.LeftBorderColor;
        xssfStyle.RightBorderColor = hssfStyle.RightBorderColor;

        xssfStyle.FillPattern = hssfStyle.FillPattern;
        xssfStyle.FillForegroundColor = hssfStyle.FillForegroundColor;
        xssfStyle.FillBackgroundColor = hssfStyle.FillBackgroundColor;

        xssfStyle.Alignment = hssfStyle.Alignment;
        xssfStyle.VerticalAlignment = hssfStyle.VerticalAlignment;
        xssfStyle.Indention = hssfStyle.Indention;
        xssfStyle.WrapText = hssfStyle.WrapText;

        return xssfStyle;
    }

    public static ICell FindCell(this ISheet worksheet, string str, Dictionary<string, ICell> cellFoundCache = null)
    {
        str = _standartize(str);
        if (cellFoundCache != null && cellFoundCache.Keys.Contains(str))
            return cellFoundCache[str];

        const int maxRows = 75;
        const int maxCols = 75;
        for (int row = 0; row < maxRows; row++)
        {
            var sheetRow = worksheet.GetRow(row);
            if (sheetRow == null) continue;
            for (int col = 0; col < maxCols; col++)
            {
                var cell = sheetRow.GetCell(col);
                if (cell == null) continue;
                if (cell.CellType == CellType.String && _standartize(cell.StringCellValue).StartsWith(str))
                {
                    if (cellFoundCache != null)
                        cellFoundCache.Add(str, cell);
                    return cell;
                }
            }
        }
        throw new Exception($"Cell with text {str} not found");
    }

    public static CellRangeAddress GetMergedRegionContainingCell(this ISheet sheet, CellAddress address)
    {
        int row = address.Row, column = address.Column;
        for (int i = 0; i < sheet.NumMergedRegions; i++)
        {
            var region = sheet.GetMergedRegion(i);
            if (region.FirstRow <= row && row <= region.LastRow &&
                region.FirstColumn <= column && column <= region.LastColumn)
            {
                return region;
            }
        }
        return null;
    }

    private static string _standartize(string str)
    {
        if (string.IsNullOrWhiteSpace(str))
            return string.Empty;
        return string.Join("", str.Where(x => char.IsLetter(x) || char.IsDigit(x))).ToUpper();
    }
}