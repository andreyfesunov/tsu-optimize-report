using BackendBase.Dto;
using BackendBase.Models;
using BackendBase.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Helpers.CRUD
{
    public interface ICRUDServiceBase<TEntity, DtoEntity> where TEntity : Base
    {
        Task<TEntity> AddEntity(TEntity entity);

        Task<DtoEntity> GetById(Guid id);

        Task<ICollection<DtoEntity>> GetAll();

        Task<TEntity> Update(TEntity entity);

        Task<bool> DeleteById(Guid entityId);

        Task<PaginationDto<DtoEntity>> Search(SearchDto searchDto);
    }
}
