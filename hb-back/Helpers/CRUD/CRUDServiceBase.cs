using BackendBase.Dto;
using BackendBase.Interfaces;
using BackendBase.Models;

namespace BackendBase.Helpers.CRUD
{
    public class CRUDServiceBase<TEntity, DtoEntity> : ICRUDServiceBase<TEntity, DtoEntity> where TEntity : Base
    {
        protected IBaseRepository<TEntity, DtoEntity> _repository;

        public async Task<TEntity> AddEntity(TEntity entity)
            => await _repository.AddEntity(entity);

        public async Task<DtoEntity> GetById(Guid id)
            => await _repository.GetById(id);

        public async Task<ICollection<DtoEntity>> GetAll()
            => await _repository.GetAll();

        public async Task<TEntity> Update(TEntity entity)
            => await _repository.UpdateEntity(entity);

        public async Task<bool> DeleteById(Guid entityId)
            => await _repository.DeleteById(entityId);

        public async Task<PaginationDto<DtoEntity>> Search(SearchDto searchDto) => await _repository.Search(searchDto);
    }
}
