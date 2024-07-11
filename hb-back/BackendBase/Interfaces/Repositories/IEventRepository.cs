using BackendBase.Data;
using BackendBase.Dto;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;
using StudentHubBackend.Exceptions;

namespace BackendBase.Repositories;

public interface IEventRepository
{
    Task<Event> AddEntity(Event entity);
    Task<Event> GetById(Guid id);
    Task<ICollection<Event>> GetAll();
    Task<Event> UpdateEntity(Event entity);
    Task<bool> DeleteById(Guid entityId);
    Task<Pagination<Event>> Search(SearchDto searchDto);
    Task<bool> Save();
    IQueryable<Event> IncludeChildren(IQueryable<Event> query);
}