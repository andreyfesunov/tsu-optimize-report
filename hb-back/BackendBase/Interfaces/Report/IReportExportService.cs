using NPOI.SS.UserModel;

namespace BackendBase.Interfaces.Report;

public interface IReportExportService
{
    Task<IWorkbook> ExportReport(string reportId);
}