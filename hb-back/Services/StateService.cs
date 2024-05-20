using AutoMapper;
using BackendBase.Dto;
using BackendBase.Dto.CreateDto;
using BackendBase.Helpers.CRUD;
using BackendBase.Interfaces;
using BackendBase.Models;
using BackendBase.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Services;

public class StateService : CRUDServiceBase<State, StateDto>, IStateService
{
    DepartmentRepository _departmentRepository;
    StateRepository _stateRepository;
    UserRepository _userRepository;
    StateUserRepository _stateUserRepository;

    public StateService(
        StateRepository repository,
        DepartmentRepository departmentRepository,
        UserRepository userRepository,
        StateUserRepository stateUserRepository,
        IMapper mapper
        ) : base(mapper)
    {
        _repository = repository;
        _departmentRepository = departmentRepository;
        _stateRepository = repository;
        _userRepository = userRepository;
        _stateUserRepository = stateUserRepository;
    }

    public async Task<State> AddStateWithCreateDto(StateCreateDto stateCreateDto)
    {
        var state = _mapper.Map<State>(stateCreateDto);
        var test = await _departmentRepository.Search(new SearchDto { PageNumber = 1, PageSize = 1 });
        state.DepartmentId = test.Entities.First().Id;
        var model = await _stateRepository.AddEntity(state);
        return model;
    }

    public async Task<bool> Assign(StateUserCreateDto stateUserCreateDto)
    {
        var state = await _stateRepository.GetByIdRoot(Guid.Parse(stateUserCreateDto.StateId));
        var userDto = await _userRepository.GetById(Guid.Parse(stateUserCreateDto.UserId));
        if (state == null || userDto == null)
            return false;
        if (state.Count < 1)
            return false;

        state.Count -= 1;
        await _stateRepository.UpdateEntity(state);

        var stateUser = _mapper.Map<StateUser>(stateUserCreateDto);
        stateUser.Rate = 1.0;

        await _stateUserRepository.AddEntity(stateUser);

        return true;
    }
}