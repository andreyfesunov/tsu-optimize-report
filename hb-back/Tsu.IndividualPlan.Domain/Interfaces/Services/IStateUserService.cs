using Tsu.IndividualPlan.Domain.Models;
using Tsu.IndividualPlan.Domain.Models.Business;
using Tsu.IndividualPlan.Domain.Models.Project;

namespace Tsu.IndividualPlan.Domain.Interfaces.Services;

public interface IStateUserService
{
    Task<Pagination<UserAllStates>> GetUserAllStates(Search search);
    Task<StateUser> GetById(Guid id, int? semestrId = null);
    Task<Pagination<StateUser>> Search(Search search);
}