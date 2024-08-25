using Tsu.IndividualPlan.WebApi.Dto;
using Tsu.IndividualPlan.WebApi.Models;

namespace Tsu.IndividualPlan.WebApi.Interfaces.Services;

public interface IStateUserService
{
    Task<StateUser> GetById(Guid id);
    Task<Pagination<StateUser>> Search(SearchDto searchDto);
}