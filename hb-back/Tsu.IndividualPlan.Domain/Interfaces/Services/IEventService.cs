using Tsu.IndividualPlan.Domain.Dto.Event;
using Tsu.IndividualPlan.Domain.Models.Business;

namespace Tsu.IndividualPlan.Domain.Interfaces.Services;

public interface IEventService
{
    Task<Event> AddEntity(EventCreateDto dto);
    Task<Event> Update(EventUpdateDto dto);
    Task<bool> DeleteById(Guid entityId);
}