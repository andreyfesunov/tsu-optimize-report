using BackendBase.Dto;
using BackendBase.Interfaces;
using BackendBase.Models;

namespace BackendBase.Helpers.CRUD
{
    public class CRUDServiceBase<TEntity> : ICRUDServiceBase<TEntity> where TEntity : Base
    {
        protected IBaseRepository<TEntity> _repository;

        public async Task<TEntity> AddEntity(TEntity entity)
            => await _repository.AddEntity(entity);

        public async Task<TEntity> GetById(Guid id)
            => await _repository.GetById(id);

        public async Task<ICollection<TEntity>> GetAll()
            => await _repository.GetAll();

        public async Task<TEntity> Update(TEntity entity)
            => await _repository.UpdateEntity(entity);

        public async Task<bool> DeleteById(Guid entityId)
            => await _repository.DeleteById(entityId);

        public async Task<PaginationDto<TEntity>> Search(SearchDto searchDto) => await _repository.Search(searchDto);
    }
}
