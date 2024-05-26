using AutoMapper;
using BackendBase.Dto;
using BackendBase.Interfaces;
using BackendBase.Models;

namespace BackendBase.Helpers.CRUD;

public class CRUDServiceBase<TEntity, DtoEntity> : ICRUDServiceBase<TEntity, DtoEntity> where TEntity : Base
{
    protected readonly IMapper _mapper;
    protected MappingHelper<TEntity, DtoEntity> _mappingHelper;
    protected IBaseRepository<TEntity> _repository;

    public CRUDServiceBase(IMapper mapper)
    {
        _mapper = mapper;
        _mappingHelper = new MappingHelper<TEntity, DtoEntity>(_mapper);
    }

    public async Task<TEntity> AddEntity(TEntity entity)
    {
        return await _repository.AddEntity(entity);
    }

    public async Task<DtoEntity> GetById(Guid id)
    {
        return _mappingHelper.toDto(await _repository.GetById(id));
    }

    public async Task<ICollection<DtoEntity>> GetAll()
    {
        return _mappingHelper.toDto(await _repository.GetAll());
    }

    public async Task<TEntity> Update(TEntity entity)
    {
        return await _repository.UpdateEntity(entity);
    }

    public async Task<bool> DeleteById(Guid entityId)
    {
        return await _repository.DeleteById(entityId);
    }

    public async Task<PaginationDto<DtoEntity>> Search(SearchDto searchDto)
    {
        return _mappingHelper.paginationToDto(await _repository.Search(searchDto));
    }
}