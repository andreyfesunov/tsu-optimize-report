using Tsu.IndividualPlan.Domain.Models.Business;
using Tsu.IndividualPlan.Domain.Models.Project;

namespace Tsu.IndividualPlan.Domain.Interfaces.Repositories;

public interface IStateRepository
{
    Task<State> GetById(Guid id);
    Task<ICollection<State>> GetAll();
    Task<State> AddEntity(State entity);
    Task<Pagination<State>> Search(Search search);
    Task<State> UpdateEntity(State entity);
    Task<IEnumerable<State>> GetById(IEnumerable<Guid> ids);
}