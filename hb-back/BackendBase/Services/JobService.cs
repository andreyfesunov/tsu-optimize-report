using AutoMapper;
using BackendBase.Dto;
using BackendBase.Helpers;
using BackendBase.Interfaces;
using BackendBase.Models;
using BackendBase.Repositories;

namespace BackendBase.Services;

public class JobService : IJobService
{
    protected readonly IMapper _mapper;
    protected MappingHelper<Job, JobDto> _mappingHelper;
    protected IBaseRepository<Job> _repository;

    public JobService(JobRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
        _mappingHelper = new MappingHelper<Job, JobDto>(_mapper);
    }

    public async Task<Job> AddEntity(Job entity)
    {
        return await _repository.AddEntity(entity);
    }

    public async Task<JobDto> GetById(Guid id)
    {
        return _mappingHelper.toDto(await _repository.GetById(id));
    }

    public async Task<ICollection<JobDto>> GetAll()
    {
        return _mappingHelper.toDto(await _repository.GetAll());
    }

    public async Task<Job> Update(Job entity)
    {
        return await _repository.UpdateEntity(entity);
    }

    public async Task<bool> DeleteById(Guid entityId)
    {
        return await _repository.DeleteById(entityId);
    }

    public async Task<PaginationDto<JobDto>> Search(SearchDto searchDto)
    {
        return _mappingHelper.paginationToDto(await _repository.Search(searchDto));
    }
}