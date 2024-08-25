using Tsu.IndividualPlan.WebApi.Dto;
using Tsu.IndividualPlan.WebApi.Models;

namespace Tsu.IndividualPlan.WebApi.Interfaces.Repositories;

public interface IStateUserRepository
{
    Task<StateUser> GetById(Guid id);
    Task<ICollection<StateUser>> GetAll();
    public Task<StateUser> AddEntity(StateUser entity);
    Task<Pagination<StateUser>> Search(SearchDto searchDto);
}