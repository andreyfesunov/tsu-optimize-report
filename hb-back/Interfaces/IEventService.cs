using BackendBase.Dto;
using BackendBase.Dto.Event;
using BackendBase.Helpers.CRUD;
using BackendBase.Models;

namespace BackendBase.Interfaces;

public interface IEventService : ICRUDServiceBase<Event, EventDto>
{
    Task<Event> Update(EventUpdateDto entity);
}