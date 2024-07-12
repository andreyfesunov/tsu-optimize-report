using BackendBase.Dto;
using BackendBase.Dto.CreateDto;
using BackendBase.Models;

namespace BackendBase.Interfaces.Services;

public interface IStateService
{
    Task<string> Create(StateCreateDto dto);
    Task<bool> Assign(StateUserCreateDto dto);
    Task<Pagination<State>> Search(SearchDto searchDto);
}
