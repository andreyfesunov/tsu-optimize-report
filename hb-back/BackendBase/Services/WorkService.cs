using AutoMapper;
using BackendBase.Dto;
using BackendBase.Helpers;
using BackendBase.Interfaces.Services;
using BackendBase.Models;
using BackendBase.Repositories;

namespace BackendBase.Services;

public class WorkService : IWorkService
{
    protected readonly IMapper _mapper;
    protected MappingHelper<Work, WorkDto> _mappingHelper;
    protected IWorkRepository _repository;

    public WorkService(IWorkRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
        _mappingHelper = new MappingHelper<Work, WorkDto>(_mapper);
    }

    public async Task<Work> AddEntity(Work entity)
    {
        return await _repository.AddEntity(entity);
    }

    public async Task<WorkDto> GetById(Guid id)
    {
        return _mappingHelper.ToDto(await _repository.GetById(id));
    }

    public async Task<ICollection<WorkDto>> GetAll()
    {
        return _mappingHelper.ToDto(await _repository.GetAll());
    }

    public async Task<Work> Update(Work entity)
    {
        return await _repository.UpdateEntity(entity);
    }

    public async Task<bool> DeleteById(Guid entityId)
    {
        return await _repository.DeleteById(entityId);
    }

    public async Task<Pagination<WorkDto>> Search(SearchDto searchDto)
    {
        return _mappingHelper.ToDto(await _repository.Search(searchDto));
    }
}