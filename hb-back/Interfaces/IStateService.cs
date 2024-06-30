using BackendBase.Dto;
using BackendBase.Dto.CreateDto;
using BackendBase.Models;

namespace BackendBase.Interfaces;

public interface IStateService
{
    Task<State> AddEntity(State entity);
    Task<StateDto> GetById(Guid id);
    Task<ICollection<StateDto>> GetAll();
    Task<State> Update(State entity);
    Task<bool> DeleteById(Guid entityId);
    Task<PaginationDto<StateDto>> Search(SearchDto searchDto);
    Task<State> AddStateWithCreateDto(StateCreateDto stateCreateDto);
    Task<bool> Assign(StateUserCreateDto stateUserCreateDto);
}