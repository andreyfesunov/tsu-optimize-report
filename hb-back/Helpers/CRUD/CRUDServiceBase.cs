using AutoMapper;
using BackendBase.Dto;
using BackendBase.Interfaces;
using BackendBase.Models;

namespace BackendBase.Helpers.CRUD
{
    public class CRUDServiceBase<TEntity, DtoEntity> : ICRUDServiceBase<TEntity, DtoEntity> where TEntity : Base
    {
        protected IBaseRepository<TEntity> _repository;
        protected readonly IMapper _mapper;

        public CRUDServiceBase(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<TEntity> AddEntity(TEntity entity)
            => await _repository.AddEntity(entity);

        public async Task<DtoEntity> GetById(Guid id)
            => MappingHelper<TEntity, DtoEntity>.toDto(await _repository.GetById(id), _mapper);

        public async Task<ICollection<DtoEntity>> GetAll()
            => MappingHelper<TEntity, DtoEntity>.toDto(await _repository.GetAll(), _mapper);

        public async Task<TEntity> Update(TEntity entity)
            => await _repository.UpdateEntity(entity);

        public async Task<bool> DeleteById(Guid entityId)
            => await _repository.DeleteById(entityId);

        public async Task<PaginationDto<DtoEntity>> Search(SearchDto searchDto) => MappingHelper<TEntity, DtoEntity>.paginationToDto(await _repository.Search(searchDto), _mapper);
    }
}
