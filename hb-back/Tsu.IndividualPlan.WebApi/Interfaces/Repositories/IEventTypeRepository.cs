using Tsu.IndividualPlan.WebApi.Dto;
using Tsu.IndividualPlan.WebApi.Models;

namespace Tsu.IndividualPlan.WebApi.Interfaces.Repositories;

public interface IEventTypeRepository
{
    Task<Pagination<EventType>> Search(Guid activityId, SearchDto searchDto);
    Task<ICollection<EventType>> GetAll();
    Task<ICollection<EventType>> GetAll(Func<EventType, bool> predicate);
}