using BackendBase.Dto;
using BackendBase.Models;

namespace BackendBase.Interfaces.Repositories;

public interface IEventTypeRepository
{
    Task<Pagination<EventType>> Search(Guid activityId, SearchDto searchDto);
    Task<ICollection<EventType>> GetAll();
    Task<ICollection<EventType>> GetAll(Func<EventType, bool> predicate);
}
