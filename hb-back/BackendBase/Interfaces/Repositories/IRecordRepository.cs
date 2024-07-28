using BackendBase.Models;

namespace BackendBase.Interfaces.Repositories;

public interface IRecordRepository
{
    Task<Record[]> Get(Guid stateUserId);
    IQueryable<Record> IncludeChildren(IQueryable<Record> query);
    Task<Record> AddEntity(Record entity);
}
