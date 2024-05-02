using BackendBase.Models;
using BackendBase.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Helpers
{
    public class CRUDServiceBase<TEntity> : ICRUDServiceBase<TEntity> where TEntity : Base
    {
        protected BaseRepository<TEntity> _repository;

        public async Task<TEntity> AddEntity(TEntity entity)
            => await _repository.AddEntity(entity);

        public async Task<TEntity> GetById(int id)
            => await _repository.GetById(id);

        public async Task<TEntity> Update(TEntity entity)
            => await _repository.UpdateEntity(entity);

        public async Task<bool> Delete(TEntity entity)
            => await _repository.Delete(entity);
    }
}
