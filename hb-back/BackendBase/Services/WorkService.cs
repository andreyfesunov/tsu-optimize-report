using AutoMapper;
using BackendBase.Dto;
using BackendBase.Interfaces.Repositories;
using BackendBase.Interfaces.Services;
using BackendBase.Models;

namespace BackendBase.Services;

public class WorkService : IWorkService
{
    protected readonly IMapper _mapper;
    protected readonly IWorkRepository _repository;

    public WorkService(IWorkRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Work> AddEntity(Work entity)
    {
        return await _repository.AddEntity(entity);
    }

    public async Task<WorkDto> GetById(Guid id)
    {
        return _mapper.Map<WorkDto>(await _repository.GetById(id));
    }

    public async Task<ICollection<WorkDto>> GetAll()
    {
        return (await _repository.GetAll()).Select(u => _mapper.Map<WorkDto>(u)).ToList();
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
        var result = await _repository.Search(searchDto);
        return new Pagination<WorkDto>
        {
            PageNumber = result.PageNumber,
            PageSize = result.PageSize,
            TotalPages = result.TotalPages,
            Entities = result.Entities.Select(u => _mapper.Map<WorkDto>(u)).ToList()
        };
    }
}
