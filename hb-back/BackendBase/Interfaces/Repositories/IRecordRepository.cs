using BackendBase.Data;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Repositories;

public interface IRecordRepository
{
    Task<Record[]> Get(Guid stateUserId);
    IQueryable<Record> IncludeChildren(IQueryable<Record> query);
    Task<Record> AddEntity(Record entity);
}