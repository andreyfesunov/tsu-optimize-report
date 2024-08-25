using Tsu.IndividualPlan.WebApi.Dto;
using Tsu.IndividualPlan.WebApi.Models;

namespace Tsu.IndividualPlan.WebApi.Interfaces.Repositories;

public interface IStateRepository
{
    Task<State> GetById(Guid id);
    Task<ICollection<State>> GetAll();
    public Task<State> AddEntity(State entity);
    Task<Pagination<State>> Search(SearchDto searchDto);
    Task<State> UpdateEntity(State entity);
}