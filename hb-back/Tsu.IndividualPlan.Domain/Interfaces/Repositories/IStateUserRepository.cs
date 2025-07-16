using Tsu.IndividualPlan.Domain.Models.Business;
using Tsu.IndividualPlan.Domain.Models.Project;

namespace Tsu.IndividualPlan.Domain.Interfaces.Repositories;

public interface IStateUserRepository
{
    Task<StateUser> GetById(Guid id, int? semestrId = null);
    Task<ICollection<StateUser>> GetAll();
    Task<StateUser> AddEntity(StateUser entity);
    Task<Pagination<StateUser>> Search(Search search);
    Task<IEnumerable<StateUser>> Get(IEnumerable<Guid> userIds);
}