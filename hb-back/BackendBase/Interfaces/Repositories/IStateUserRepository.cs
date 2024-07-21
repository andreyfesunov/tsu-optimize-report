using BackendBase.Dto;
using BackendBase.Models;
using MathNet.Numerics.Statistics.Mcmc;

namespace BackendBase.Interfaces.Repositories;

public interface IStateUserRepository
{
    Task<StateUser> GetById(Guid id);
    Task<ICollection<StateUser>> GetAll();
    public Task<StateUser> AddEntity(StateUser entity);
    Task<Pagination<StateUser>> Search(SearchDto searchDto);
}
