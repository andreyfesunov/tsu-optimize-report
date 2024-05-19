namespace BackendBase.Interfaces;

public interface IReportService
{
    Task<bool> CreateReport(Guid stateUserId, IFormFile file);
}