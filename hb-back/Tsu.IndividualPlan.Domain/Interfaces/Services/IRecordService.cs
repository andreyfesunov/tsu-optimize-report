using Tsu.IndividualPlan.Domain.Models.Business;

namespace Tsu.IndividualPlan.Domain.Interfaces.Services;

public interface IRecordService
{
    Task<ICollection<Record>> GetForReport(Guid stateUserId);
}