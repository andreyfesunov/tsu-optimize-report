using Tsu.IndividualPlan.Domain.Models.Business;

namespace Tsu.IndividualPlan.Domain.Interfaces.Repositories;

public interface IRecordRepository
{
    Task<Record[]> Get(Guid stateUserId);
    IQueryable<Record> IncludeChildren(IQueryable<Record> query);
    Task<Record> AddEntity(Record entity);
}