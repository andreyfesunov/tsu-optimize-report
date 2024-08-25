namespace Tsu.IndividualPlan.WebApi.Interfaces.Services.Report;

public interface IReportCreateService
{
    Task<bool> CreateReport(Guid stateUserId, IFormFile file);
}