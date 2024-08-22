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
        // TODO improve creating, add departmentId to DTO or to UserInfo
        var departments = await _departmentRepository.Search(
            new SearchDto(PageNumber: 1, PageSize: 1)
        );
        var departmentId = departments.Entities.First().Id;

        var state = new State(
            DepartmentId: departmentId,
            JobId: dto.JobId,
            Count: dto.Count,
            Hours: dto.Hours,
            StartDate: dto.StartDate,
            EndDate: dto.EndDate
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

        var stateUser = new StateUser(StateId: dto.StateId, UserId: dto.UserId, Rate: 1.0);

        await _stateUserRepository.AddEntity(stateUser);

        return true;
    }

    public async Task<Pagination<State>> Search(SearchDto searchDto) =>
        await _stateRepository.Search(searchDto);
}
