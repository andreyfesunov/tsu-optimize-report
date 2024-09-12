using Tsu.IndividualPlan.Domain.Models.Business;
using Tsu.IndividualPlan.Domain.Models.Project;

namespace Tsu.IndividualPlan.Domain.Interfaces.Services;

public interface IStateUserService
{
    Task<StateUser> GetById(Guid id);
    Task<Pagination<StateUser>> Search(Search search);
}