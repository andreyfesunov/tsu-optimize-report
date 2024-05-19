using BackendBase.Dto;
using BackendBase.Models;

namespace BackendBase.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : Base
    {
        Task<TEntity> GetById(Guid id);
        Task<TEntity> GetByIdRoot(Guid id);
        ICollection<TEntity> SearchEntity(Func<TEntity, bool> predicate);
        Task<ICollection<TEntity>> GetAll();
        Task<bool> DoesExist(Guid id);
        Task<TEntity> AddEntity(TEntity entity);
        Task<TEntity> UpdateEntity(TEntity entity);
        Task<bool> Delete(TEntity entity);
        Task<bool> DeleteById(Guid entityId);
        Task<bool> Save();
        Task<PaginationDto<TEntity>> SearchRoot(SearchDto searchDto);
        Task<PaginationDto<TEntity>> Search(SearchDto searchDto);
    }
}
