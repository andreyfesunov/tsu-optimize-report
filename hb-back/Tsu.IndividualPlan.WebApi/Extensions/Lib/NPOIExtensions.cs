using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Tsu.IndividualPlan.WebApi.Extensions.Lib;

public static class NPOIExtensions
{
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