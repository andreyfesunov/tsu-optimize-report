using NPOI.SS.UserModel;

namespace Tsu.IndividualPlan.WebApi.Interfaces.Services.Report;

public interface IReportExportService
{
    Task<IWorkbook> ExportReport(string reportId);
}