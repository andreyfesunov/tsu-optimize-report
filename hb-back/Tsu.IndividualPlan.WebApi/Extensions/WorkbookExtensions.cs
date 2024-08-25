using NPOI.SS.UserModel;

namespace Tsu.IndividualPlan.WebApi.Extensions;

public static class WorkbookExtensions
{
    public static IRow GetRowSafe(this ISheet sheet, int row)
    {
        return sheet.GetRow(row) ?? sheet.CreateRow(row);
    }

    public static ICell GetCellSafe(this IRow row, int col)
    {
        return row.GetCell(col) ?? row.CreateCell(col);
    }
}