using Tsu.IndividualPlan.Domain.Interfaces.Repositories;
using Tsu.IndividualPlan.Domain.Interfaces.Services;
using Tsu.IndividualPlan.Domain.Models;
using Tsu.IndividualPlan.Domain.Models.Business;
using Tsu.IndividualPlan.Domain.Models.Project;

namespace Tsu.IndividualPlan.Domain.Services;

public class StateUserService(IStateUserRepository stateUserRepository,
        IUserRepository userRepository,
        IStateRepository stateRepository) : IStateUserService
{
    public async Task<StateUser> GetById(Guid id)
    {
        return await stateUserRepository.GetById(id);
    }

    public async Task<Pagination<UserAllStates>> GetUserAllStates(Search search)
    {
        var users = await userRepository.Search(search);
        var stateUsers = await stateUserRepository.Get(users.Entities.Select(u => u.Id));
        var states = await stateRepository.GetById(stateUsers.Select(su => su.StateId));

        var userAllStates = users.Entities.Select(u =>
        {
            return new UserAllStates
            {
                User = u,
                States = states.Where(s => stateUsers.Where(su => su.UserId == u.Id).Select(su => su.StateId).Contains(s.Id))
            };
        });

        return new Pagination<UserAllStates>(
            search.PageNumber,
            search.PageSize,
            users.TotalPages,
            userAllStates.ToList()
        );
    }

    public async Task<Pagination<StateUser>> Search(Search search)
    {
        return await stateUserRepository.Search(search);
    }
}