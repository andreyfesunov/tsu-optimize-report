using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace BackendBase.Extensions.Lib;

public static class NPOIExtensions
{
    public static XSSFCellStyle toXSSF(this HSSFCellStyle hssfStyle, IWorkbook xssfWorkbook)
    {
        XSSFCellStyle xssfStyle = (XSSFCellStyle)xssfWorkbook.CreateCellStyle();

        xssfStyle.BorderBottom = (BorderStyle)hssfStyle.BorderBottom;
        xssfStyle.BorderTop = (BorderStyle)hssfStyle.BorderTop;
        xssfStyle.BorderLeft = (BorderStyle)hssfStyle.BorderLeft;
        xssfStyle.BorderRight = (BorderStyle)hssfStyle.BorderRight;
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
