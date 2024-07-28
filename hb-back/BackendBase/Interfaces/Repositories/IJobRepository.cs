using BackendBase.Dto;
using BackendBase.Models;

namespace BackendBase.Interfaces.Repositories
{
    public interface IJobRepository
    {
        IQueryable<Job> IncludeChildren(IQueryable<Job> query);
        Task<Job> AddEntity(Job entity);
        Task<Job> GetById(Guid id);
        Task<ICollection<Job>> GetAll();
        Task<Job> UpdateEntity(Job entity);
        Task<bool> DeleteById(Guid entityId);
        Task<Pagination<Job>> Search(SearchDto searchDto);
        Task<bool> Save();
    }
}
