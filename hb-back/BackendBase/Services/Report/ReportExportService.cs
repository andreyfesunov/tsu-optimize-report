using BackendBase.Interfaces.Report;
using BackendBase.Repositories;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace BackendBase.Services.Report;

public class ReportExportService : IReportExportService
{
    private readonly List<Action> _sheetBuilders = new();

    private readonly UserRepository _userRepo;

    public ReportExportService(
        UserRepository userRepo
    )
    {
        _userRepo = userRepo;
    }

    public async Task<IWorkbook> ExportReport(string reportId)
    {
        var user = await _userRepo.GetUserByEmail("");
        var workbook = new XSSFWorkbook();
        return workbook;
    }
}