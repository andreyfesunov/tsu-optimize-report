using NPOI.SS.UserModel;

namespace Tsu.IndividualPlan.Transfer.Interfaces.Report;

public interface IReportExportService
{
    Task<IWorkbook> ExportReport(string reportId);
}