using Tsu.IndividualPlan.Domain.Models.Business;
using Tsu.IndividualPlan.Domain.Models.Project;

namespace Tsu.IndividualPlan.Domain.Interfaces.Repositories;

public interface IEventTypeRepository
{
    Task<Pagination<EventType>> Search(Guid activityId, Search search);
    Task<ICollection<EventType>> GetAll();
    Task<ICollection<EventType>> GetAll(Func<EventType, bool> predicate);
}