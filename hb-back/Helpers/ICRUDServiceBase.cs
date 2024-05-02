using BackendBase.Models;
using BackendBase.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Helpers
{
    public interface ICRUDServiceBase<TEntity> where TEntity : Base
    {
        Task<TEntity> AddEntity(TEntity entity);

        Task<TEntity> GetById(Guid id);

        Task<TEntity> Update(TEntity entity);

        Task<bool> Delete(TEntity entity);
    }
}
