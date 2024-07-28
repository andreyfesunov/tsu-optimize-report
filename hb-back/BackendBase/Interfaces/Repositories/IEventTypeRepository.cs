using BackendBase.Dto;
using BackendBase.Models;

namespace BackendBase.Interfaces.Repositories;

public interface IEventTypeRepository
{
    IQueryable<EventType> IncludeChildren(IQueryable<EventType> query);
    Task<Pagination<EventType>> Search(Guid activityId, SearchDto searchDto);
    Task<EventType> AddEntity(EventType entity);
    Task<EventType> GetById(Guid id);
    Task<ICollection<EventType>> GetAll();
    Task<EventType> UpdateEntity(EventType entity);
    Task<bool> DeleteById(Guid entityId);
    Task<Pagination<EventType>> Search(SearchDto searchDto);
    Task<bool> Save();
    Task<ICollection<EventType>> GetAll(Func<EventType, bool> predicate);
}
