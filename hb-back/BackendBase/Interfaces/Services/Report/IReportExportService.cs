using NPOI.SS.UserModel;

namespace BackendBase.Interfaces.Services.Report;

public interface IReportExportService
{
    Task<IWorkbook> ExportReport(string reportId);
}