using Tsu.IndividualPlan.WebApi.Models;

namespace Tsu.IndividualPlan.WebApi.Interfaces.Services;

public interface IRecordService
{
    Task<ICollection<Record>> GetForReport(Guid stateUserId);
}