using AutoMapper;
using BackendBase.Dto;
using BackendBase.Interfaces.Repositories;
using BackendBase.Interfaces.Services;
using BackendBase.Models;

namespace BackendBase.Services;

public class JobService : IJobService
{
    protected readonly IMapper _mapper;
    protected IJobRepository _repository;

    public JobService(IJobRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Job> AddEntity(Job entity)
    {
        return await _repository.AddEntity(entity);
    }

    public async Task<JobDto> GetById(Guid id)
    {
        return _mapper.Map<JobDto>(await _repository.GetById(id));
    }

    public async Task<ICollection<JobDto>> GetAll()
    {
        return (await _repository.GetAll()).Select(u => _mapper.Map<JobDto>(u)).ToList();
    }

    public async Task<Job> Update(Job entity)
    {
        return await _repository.UpdateEntity(entity);
    }

    public async Task<bool> DeleteById(Guid entityId)
    {
        return await _repository.DeleteById(entityId);
    }

    public async Task<Pagination<JobDto>> Search(SearchDto searchDto)
    {
        var result = await _repository.Search(searchDto);
        return new Pagination<JobDto>
        {
            PageNumber = result.PageNumber,
            PageSize = result.PageSize,
            TotalPages = result.TotalPages,
            Entities = result.Entities.Select(u => _mapper.Map<JobDto>(u)).ToList()
        };
    }
}
