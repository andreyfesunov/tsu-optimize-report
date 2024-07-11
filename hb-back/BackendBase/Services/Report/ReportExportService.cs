using BackendBase.Interfaces.Repositories;
using BackendBase.Interfaces.Services.Report;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace BackendBase.Services.Report;

public class ReportExportService : IReportExportService
{
    private readonly List<Action> _sheetBuilders = new();

    private readonly IUserRepository _userRepo;

    public ReportExportService(
        IUserRepository userRepo
    )
    {
        _userRepo = userRepo;
    }

    public async Task<IWorkbook> ExportReport(string reportId)
    {
        var user = await _userRepo.GetByEmail("");
        var workbook = new XSSFWorkbook();
        return workbook;
    }
}
