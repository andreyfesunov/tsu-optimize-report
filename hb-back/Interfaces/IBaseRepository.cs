using BackendBase.Dto;
using BackendBase.Models;

namespace BackendBase.Interfaces
{
    public interface IBaseRepository<TEntity, DtoEntity> where TEntity : Base
    {
        Task<DtoEntity> GetById(Guid id);
        ICollection<TEntity> SearchEntity(Func<TEntity, bool> predicate);
        Task<ICollection<DtoEntity>> GetAll();
        Task<bool> DoesExist(Guid id);
        Task<TEntity> AddEntity(TEntity entity);
        Task<TEntity> UpdateEntity(TEntity entity);
        Task<bool> Delete(TEntity entity);
        Task<bool> DeleteById(Guid entityId);
        Task<bool> Save();
        Task<PaginationDto<DtoEntity>> Search(SearchDto searchDto);
    }
}
