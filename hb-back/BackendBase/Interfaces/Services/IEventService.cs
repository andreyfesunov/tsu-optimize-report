using BackendBase.Dto.Event;
using BackendBase.Models;

namespace BackendBase.Interfaces.Services;

public interface IEventService
{
    Task<Event> AddEntity(EventCreateDto dto);
    Task<Event> Update(EventUpdateDto dto);
    Task<bool> DeleteById(Guid entityId);
}
