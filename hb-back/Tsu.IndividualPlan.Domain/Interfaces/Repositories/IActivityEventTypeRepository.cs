using Tsu.IndividualPlan.Domain.Models.Business;
using Tsu.IndividualPlan.Domain.Models.Project;

namespace Tsu.IndividualPlan.Domain.Interfaces.Repositories;

public interface IActivityEventTypeRepository
{
    IQueryable<ActivityEventType> IncludeChildren(IQueryable<ActivityEventType> query);
    Task<bool> Validate(ActivityEventType model);
    Task<ActivityEventType> AddEntity(ActivityEventType entity);
    Task<ActivityEventType> GetById(Guid id);
    Task<ICollection<ActivityEventType>> GetAll();
    Task<ActivityEventType> UpdateEntity(ActivityEventType entity);
    Task<bool> DeleteById(Guid entityId);
    Task<Pagination<ActivityEventType>> Search(Search search);
    Task<bool> Save();
    Task<ICollection<ActivityEventType>> GetAll(Func<ActivityEventType, bool> predicate);
    Task<bool> DeleteBatch(IEnumerable<ActivityEventType> entities);
}