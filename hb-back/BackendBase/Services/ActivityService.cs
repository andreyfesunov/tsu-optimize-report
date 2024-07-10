using AutoMapper;
using BackendBase.Dto;
using BackendBase.Helpers;
using BackendBase.Interfaces.Repositories.Common;
using BackendBase.Interfaces.Services;
using BackendBase.Models;
using BackendBase.Repositories;

namespace BackendBase.Services;

public class ActivityService : IActivityService
{
    protected readonly IMapper _mapper;
    protected MappingHelper<Activity, ActivityDto> _mappingHelper;
    protected IBaseRepository<Activity> _repository;

    public ActivityService(ActivityRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
        _mappingHelper = new MappingHelper<Activity, ActivityDto>(_mapper);
    }

    public async Task<Activity> AddEntity(Activity entity)
    {
        return await _repository.AddEntity(entity);
    }

    public async Task<ActivityDto> GetById(Guid id)
    {
        return _mappingHelper.ToDto(await _repository.GetById(id));
    }

    public async Task<ICollection<ActivityDto>> GetAll()
    {
        return _mappingHelper.ToDto(await _repository.GetAll());
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
        return _mappingHelper.ToDto(await _repository.Search(searchDto));
    }
}