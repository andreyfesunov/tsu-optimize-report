using BackendBase.Dto;
using BackendBase.Dto.CreateDto;
using BackendBase.Interfaces.Repositories;
using BackendBase.Interfaces.Services;
using BackendBase.Models;

namespace BackendBase.Services;

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
        var state = new State {
            JobId = Guid.Parse(dto.JobId),
            Count = dto.Count,
            Hours = dto.Hours,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate
        };

        // TODO improve creating, add departmentId to DTO or to UserInfo
        var test = await _departmentRepository.Search(new SearchDto { PageNumber = 1, PageSize = 1 });
        state.DepartmentId = test.Entities.First().Id;

        return (await _stateRepository.AddEntity(state)).Id.ToString();
    }

    public async Task<bool> Assign(StateUserCreateDto stateUserCreateDto)
    {
        var state = await _stateRepository.GetById(Guid.Parse(stateUserCreateDto.StateId));
        var userDto = await _userRepository.GetById(Guid.Parse(stateUserCreateDto.UserId));
        if (state == null || userDto == null)
            return false;
        if (state.Count < 1)
            return false;

        state.Count -= 1;
        await _stateRepository.UpdateEntity(state);

        var stateUser = new StateUser {
            StateId = Guid.Parse(stateUserCreateDto.StateId),
            UserId = Guid.Parse(stateUserCreateDto.UserId)
        };
        stateUser.Rate = 1.0;

        await _stateUserRepository.AddEntity(stateUser);

        return true;
    }
}
