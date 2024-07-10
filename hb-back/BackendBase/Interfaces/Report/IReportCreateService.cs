namespace BackendBase.Interfaces.Report;

public interface IReportCreateService
{
    Task<bool> CreateReport(Guid stateUserId, IFormFile file);
}