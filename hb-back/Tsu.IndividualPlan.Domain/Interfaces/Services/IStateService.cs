using Tsu.IndividualPlan.Domain.Dto.IndividualPlan;
using Tsu.IndividualPlan.Domain.Dto.State;
using Tsu.IndividualPlan.Domain.Models.Business;
using Tsu.IndividualPlan.Domain.Models.Project;

namespace Tsu.IndividualPlan.Domain.Interfaces.Services;

public interface IStateService
{
    Task<string> Create(StateCreateDto dto);
    Task<bool> Assign(IndividualPlanCreateDto dto);
    Task<Pagination<State>> Search(Search search);
}