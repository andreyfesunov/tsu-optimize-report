using AutoMapper;
using BackendBase.Dto;
using BackendBase.Interfaces;
using BackendBase.Models;
using BackendBase.Helpers;

namespace BackendBase.Helpers.CRUD
{
    public class CRUDServiceBase<TEntity, DtoEntity> : ICRUDServiceBase<TEntity, DtoEntity> where TEntity : Base
    {
        protected IBaseRepository<TEntity> _repository;
        protected readonly IMapper _mapper;
        protected MappingHelper<TEntity, DtoEntity> _mappingHelper;

        public CRUDServiceBase(IMapper mapper)
        {
            _mapper = mapper;
            _mappingHelper = new MappingHelper<TEntity, DtoEntity>(_mapper);
        }

        public async Task<TEntity> AddEntity(TEntity entity)
            => await _repository.AddEntity(entity);

        public async Task<DtoEntity> GetById(Guid id)
            => _mappingHelper.toDto(await _repository.GetById(id));

        public async Task<ICollection<DtoEntity>> GetAll()
            => _mappingHelper.toDto(await _repository.GetAll());

        public async Task<TEntity> Update(TEntity entity)
            => await _repository.UpdateEntity(entity);

        public async Task<bool> DeleteById(Guid entityId)
            => await _repository.DeleteById(entityId);

        public async Task<PaginationDto<DtoEntity>> Search(SearchDto searchDto) => _mappingHelper.paginationToDto(await _repository.Search(searchDto));
    }
}
