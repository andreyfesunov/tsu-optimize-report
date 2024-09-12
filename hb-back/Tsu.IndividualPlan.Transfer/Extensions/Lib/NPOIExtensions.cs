using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
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
}