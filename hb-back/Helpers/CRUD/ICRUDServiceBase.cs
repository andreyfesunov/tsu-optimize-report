using BackendBase.Dto;
using BackendBase.Models;
using BackendBase.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Helpers.CRUD
{
    public interface ICRUDServiceBase<TEntity> where TEntity : Base
    {
        Task<TEntity> AddEntity(TEntity entity);

        Task<TEntity> GetById(Guid id);

        Task<ICollection<TEntity>> GetAll();

        Task<TEntity> Update(TEntity entity);

        Task<bool> DeleteById(Guid entityId);

        Task<PaginationDto<TEntity>> Search(SearchDto searchDto);
    }
}
