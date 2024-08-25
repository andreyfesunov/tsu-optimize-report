using Tsu.IndividualPlan.WebApi.Dto;
using Tsu.IndividualPlan.WebApi.Interfaces.Repositories;
using Tsu.IndividualPlan.WebApi.Interfaces.Services;
using Tsu.IndividualPlan.WebApi.Models;

namespace Tsu.IndividualPlan.WebApi.Services;

public class StateUserService : IStateUserService
{
    private readonly IStateUserRepository _stateUserRepository;

    public StateUserService(IStateUserRepository stateUserRepository)
    {
        _stateUserRepository = stateUserRepository;
    }


    public async Task<StateUser> GetById(Guid id)
    {
        return await _stateUserRepository.GetById(id);
    }

    public async Task<Pagination<StateUser>> Search(SearchDto searchDto)
    {
        return await _stateUserRepository.Search(searchDto);
    }
}