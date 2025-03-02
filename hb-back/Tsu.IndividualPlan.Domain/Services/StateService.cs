using Tsu.IndividualPlan.Domain.Dto.IndividualPlan;
using Tsu.IndividualPlan.Domain.Dto.State;
using Tsu.IndividualPlan.Domain.Interfaces.Repositories;
using Tsu.IndividualPlan.Domain.Interfaces.Services;
using Tsu.IndividualPlan.Domain.Models.Business;
using Tsu.IndividualPlan.Domain.Models.Project;
using Tsu.IndividualPlan.Data.Context;

namespace Tsu.IndividualPlan.Domain.Services;

public class StateService(
    IStateRepository repository,
    IDepartmentRepository departmentRepository,
    IUserRepository userRepository,
    IStateUserRepository stateUserRepository,
    DataContext context)
    : IStateService
{
    public async Task<string> Create(StateCreateDto dto)
    {
        // TODO improve creating, add departmentId to DTO or to UserInfo
        var departments = await departmentRepository.Search(
            new Search(1, 1)
        );
        var departmentId = departments.Entities.First().Id;

        var state = new State(
            departmentId,
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
        using (var transaction = context.Database.BeginTransaction())
        {
            try
            {
                var state = await repository.GetById(dto.StateId);

                if (state.Count < 1)
                    return false;

                state.Count -= 1;
                await repository.UpdateEntity(state);

                var stateUser = new StateUser(dto.StateId, dto.UserId, 1.0);

                await stateUserRepository.AddEntity(stateUser);

                await transaction.CommitAsync();

                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }

    public async Task<Pagination<State>> Search(Search search)
    {
        return await repository.Search(search);
    }
}