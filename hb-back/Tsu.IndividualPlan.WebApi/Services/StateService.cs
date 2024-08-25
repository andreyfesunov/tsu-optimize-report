using Tsu.IndividualPlan.WebApi.Dto;
using Tsu.IndividualPlan.WebApi.Dto.CreateDto;
using Tsu.IndividualPlan.WebApi.Interfaces.Repositories;
using Tsu.IndividualPlan.WebApi.Interfaces.Services;
using Tsu.IndividualPlan.WebApi.Models;

namespace Tsu.IndividualPlan.WebApi.Services;

public class StateService : IStateService
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IStateRepository _stateRepository;
    private readonly IStateUserRepository _stateUserRepository;
    private readonly IUserRepository _userRepository;

    public StateService(
        IStateRepository repository,
        IDepartmentRepository departmentRepository,
        IUserRepository userRepository,
        IStateUserRepository stateUserRepository
    )
    {
        _departmentRepository = departmentRepository;
        _stateRepository = repository;
        _userRepository = userRepository;
        _stateUserRepository = stateUserRepository;
    }

    public async Task<string> Create(StateCreateDto dto)
    {
        // TODO improve creating, add departmentId to DTO or to UserInfo
        var departments = await _departmentRepository.Search(
            new SearchDto(1, 1)
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

        return (await _stateRepository.AddEntity(state)).Id.ToString();
    }

    public async Task<bool> Assign(StateUserCreateDto dto)
    {
        var state = await _stateRepository.GetById(dto.StateId);
        var userDto = await _userRepository.GetById(dto.UserId);
        if (state == null || userDto == null)
            return false;
        if (state.Count < 1)
            return false;

        state.Count -= 1;
        await _stateRepository.UpdateEntity(state);

        var stateUser = new StateUser(dto.StateId, dto.UserId, 1.0);

        await _stateUserRepository.AddEntity(stateUser);

        return true;
    }

    public async Task<Pagination<State>> Search(SearchDto searchDto)
    {
        return await _stateRepository.Search(searchDto);
    }
}