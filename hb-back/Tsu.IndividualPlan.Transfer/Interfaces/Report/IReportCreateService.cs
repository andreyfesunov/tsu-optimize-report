using Microsoft.AspNetCore.Http;

namespace Tsu.IndividualPlan.Transfer.Interfaces.Report;

public interface IReportCreateService
{
    Task<bool> CreateReport(Guid stateUserId, IFormFile file);
}