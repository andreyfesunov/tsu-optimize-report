using AutoMapper;
using BackendBase.Dto;
using BackendBase.Helpers;
using BackendBase.Interfaces.Services;
using BackendBase.Models;
using BackendBase.Repositories;

namespace BackendBase.Services;

public class ActivityService : IActivityService
{
    protected readonly IMapper _mapper;
    protected IActivityRepository _repository;

    public ActivityService(IActivityRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Activity> AddEntity(Activity entity)
    {
        return await _repository.AddEntity(entity);
    }

    public async Task<ActivityDto> GetById(Guid id)
    {
        return _mapper.Map<ActivityDto>(await _repository.GetById(id));
    }

    public async Task<ICollection<ActivityDto>> GetAll()
    {
        return (await _repository.GetAll()).Select(u => _mapper.Map<ActivityDto>(u)).ToList();
    }

    public async Task<Activity> Update(Activity entity)
    {
        return await _repository.UpdateEntity(entity);
    }

    public async Task<bool> DeleteById(Guid entityId)
    {
        return await _repository.DeleteById(entityId);
    }

    public async Task<Pagination<ActivityDto>> Search(SearchDto searchDto)
    {
        var result = await _repository.Search(searchDto);
        return new Pagination<ActivityDto>
        {
            PageNumber = result.PageNumber,
            PageSize = result.PageSize,
            TotalPages = result.TotalPages,
            Entities = result.Entities.Select(u => _mapper.Map<ActivityDto>(u)).ToList()
        };
    }
}