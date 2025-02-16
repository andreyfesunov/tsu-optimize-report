using Tsu.IndividualPlan.Domain.Dto.IndividualPlan;
using Tsu.IndividualPlan.Domain.Dto.State;
using Tsu.IndividualPlan.Domain.Interfaces.Repositories;
using Tsu.IndividualPlan.Domain.Interfaces.Services;
using Tsu.IndividualPlan.Domain.Models.Business;
using Tsu.IndividualPlan.Domain.Models.Project;

namespace Tsu.IndividualPlan.Domain.Services;

public class StateService(
    IStateRepository repository,
    IDepartmentRepository departmentRepository,
    IUserRepository userRepository,
    IStateUserRepository stateUserRepository)
    : IStateService
{
    public async Task<string> Create(StateCreateDto dto)
    {
        var state = new State(
            dto.DepartmentId,
            dto.JobId,
            dto.Count,
            dto.Hours,
            dto.StartDate,
            dto.EndDate
        );

        return (await repository.AddEntity(state)).Id.ToString();
    }

    public async Task<bool> Assign(IndividualPlanCreateDto dto)
    {
        var state = await repository.GetById(dto.StateId);

        if (state.Count < 1)
            return false;

        state.Count -= 1;
        await repository.UpdateEntity(state);

        var stateUser = new StateUser(dto.StateId, dto.UserId, 1.0);

        await stateUserRepository.AddEntity(stateUser);

        return true;
    }

    public async Task<Pagination<State>> Search(Search search)
    {
        return await repository.Search(search);
    }
}