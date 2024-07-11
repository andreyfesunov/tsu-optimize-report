using BackendBase.Dto;
using BackendBase.Models;

namespace BackendBase.Interfaces.Repositories;

public interface IStateRepository
{
    Task<State> GetById(Guid id);
    Task<ICollection<State>> GetAll();
    public Task<State> AddEntity(State entity);
    Task<Pagination<State>> Search(SearchDto searchDto);
    Task<State> UpdateEntity(State entity);
}
