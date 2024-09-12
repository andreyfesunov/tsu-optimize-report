using Tsu.IndividualPlan.Domain.Interfaces.Repositories;
using Tsu.IndividualPlan.Domain.Interfaces.Services;
using Tsu.IndividualPlan.Domain.Models.Business;
using Tsu.IndividualPlan.Domain.Models.Project;

namespace Tsu.IndividualPlan.Domain.Services;

public class StateUserService(IStateUserRepository stateUserRepository) : IStateUserService
{
    public async Task<StateUser> GetById(Guid id)
    {
        return await stateUserRepository.GetById(id);
    }

    public async Task<Pagination<StateUser>> Search(Search search)
    {
        return await stateUserRepository.Search(search);
    }
}