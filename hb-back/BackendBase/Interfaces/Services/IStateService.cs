using BackendBase.Dto.CreateDto;

namespace BackendBase.Interfaces.Services;

public interface IStateService
{
    Task<string> Create(StateCreateDto dto);
    Task<bool> Assign(StateUserCreateDto dto);
}
