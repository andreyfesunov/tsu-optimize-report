using Tsu.IndividualPlan.WebApi.Dto;
using Tsu.IndividualPlan.WebApi.Dto.CreateDto;
using Tsu.IndividualPlan.WebApi.Models;

namespace Tsu.IndividualPlan.WebApi.Interfaces.Services;

public interface IStateService
{
    Task<string> Create(StateCreateDto dto);
    Task<bool> Assign(StateUserCreateDto dto);
    Task<Pagination<State>> Search(SearchDto searchDto);
}