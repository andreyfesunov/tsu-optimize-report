using BackendBase.Dto.Event;
using BackendBase.Models;

namespace BackendBase.Interfaces.Services;

public interface IEventService
{
    Task<Event> AddEntity(Event entity);
    Task<Event> Update(EventUpdateDto entity);
    Task<bool> DeleteById(Guid entityId);
}
