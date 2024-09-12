using Tsu.IndividualPlan.Domain.Models.Business;
using Tsu.IndividualPlan.Domain.Models.Project;

namespace Tsu.IndividualPlan.Domain.Interfaces.Repositories;

public interface IWorkRepository
{
    Task<Work> AddEntity(Work entity);
    Task<Work> GetById(Guid id);
    Task<ICollection<Work>> GetAll();
    Task<Work> UpdateEntity(Work entity);
    Task<bool> DeleteById(Guid entityId);
    Task<Pagination<Work>> Search(Search search);
    IQueryable<Work> IncludeChildren(IQueryable<Work> query);
}