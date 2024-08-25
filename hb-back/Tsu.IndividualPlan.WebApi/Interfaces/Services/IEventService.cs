using Tsu.IndividualPlan.WebApi.Dto.Event;
using Tsu.IndividualPlan.WebApi.Models;

namespace Tsu.IndividualPlan.WebApi.Interfaces.Services;

public interface IEventService
{
    Task<Event> AddEntity(EventCreateDto dto);
    Task<Event> Update(EventUpdateDto dto);
    Task<bool> DeleteById(Guid entityId);
}