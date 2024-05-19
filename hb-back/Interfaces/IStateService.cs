using BackendBase.Dto;
using BackendBase.Dto.CreateDto;
using BackendBase.Helpers.CRUD;
using BackendBase.Models;

namespace BackendBase.Interfaces;

public interface IStateService : ICRUDServiceBase<State, StateDto>
{
    Task<State> AddStateWithCreateDto(StateCreateDto stateCreateDto);

    Task<bool> Assign(StateUserCreateDto stateUserCreateDto);
}