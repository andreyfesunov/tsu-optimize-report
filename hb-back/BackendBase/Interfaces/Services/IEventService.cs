using BackendBase.Dto;
using BackendBase.Dto.Event;
using BackendBase.Models;

namespace BackendBase.Interfaces.Services;

public interface IEventService
{
    Task<Event> AddEntity(Event entity);
    Task<EventDto> GetById(Guid id);
    Task<ICollection<EventDto>> GetAll();
    Task<Event> Update(Event entity);
    Task<bool> DeleteById(Guid entityId);
    Task<Pagination<EventDto>> Search(SearchDto searchDto);
    Task<Event> Update(EventUpdateDto entity);
}