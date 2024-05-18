using BackendBase.Dto;
using BackendBase.Dto.CreateDto;
using BackendBase.Helpers.CRUD;
using BackendBase.Models;
using BackendBase.Repositories;

namespace BackendBase.Interfaces
{
    public interface IStateService : ICRUDServiceBase<State, StateDto>
    {
        Task<State> AddStateWithCreateDto(StateCreateDto stateCreateDto);

        Task<bool> SetState(StateUserCreateDto stateUserCreateDto);
    }
}
