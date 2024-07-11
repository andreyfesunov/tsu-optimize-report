using AutoMapper;
using BackendBase.Dto;
using BackendBase.Helpers;
using BackendBase.Interfaces.Repositories;
using BackendBase.Interfaces.Services;
using BackendBase.Models;
using BackendBase.Repositories;

namespace BackendBase.Services;

public class EventTypeService : IEventTypeService
{
    private readonly ActivityEventTypeRepository _activityEventTypeRepository;
    private readonly ActivityRepository _activityRepository;
    private readonly IMapper _mapper;
    private readonly EventTypeRepository _repository;
    private readonly IStateUserRepository _stateUserRepository;
    protected MappingHelper<EventType, EventTypeDto> _mappingHelper;

    public EventTypeService(
        EventTypeRepository repository,
        ActivityEventTypeRepository activityEventTypeRepository,
        ActivityRepository activityRepository,
        IStateUserRepository stateUserRepository,
        IMapper mapper
    )
    {
        _repository = repository;
        _activityEventTypeRepository = activityEventTypeRepository;
        _activityRepository = activityRepository;
        _stateUserRepository = stateUserRepository;
        _mapper = mapper;
    }

    public async Task<EventType> AddEntity(EventType entity)
    {
        return await _repository.AddEntity(entity);
    }

    public async Task<EventTypeDto> GetById(Guid id)
    {
        return _mappingHelper.ToDto(await _repository.GetById(id));
    }

    public async Task<ICollection<EventTypeDto>> GetAll()
    {
        return _mappingHelper.ToDto(await _repository.GetAll());
    }

    public async Task<EventType> Update(EventType entity)
    {
        return await _repository.UpdateEntity(entity);
    }

    public async Task<bool> DeleteById(Guid entityId)
    {
        return await _repository.DeleteById(entityId);
    }

    public async Task<Pagination<EventTypeDto>> Search(SearchDto searchDto)
    {
        return _mappingHelper.ToDto(await _repository.Search(searchDto));
    }

    public async Task<Dictionary<string, Pagination<EventTypeDto>>> SearchMap(SearchDto searchDto)
    {
        var searchMap = new Dictionary<string, Pagination<EventTypeDto>>();

        var activities = await _activityRepository.GetAll();

        foreach (var activity in activities)
            searchMap[activity.Id.ToString()] = await Search(activity.Id, searchDto);

        return searchMap;
    }

    public async Task<ICollection<EventTypeDto>> GetAllForReport(Guid stateUserId, Guid workId, bool first)
    {
        var stateUser = await _stateUserRepository.GetById(stateUserId);
        var activityIds = stateUser.Records.Select(x => x.Activity.Id);
        var eventTypeIds =
            (await _activityEventTypeRepository.GetAll(x => activityIds.Contains(x.ActivityId))).Select(x =>
                x.EventTypeId);

        return (await _repository.GetAll(x => x.WorkId == workId && (eventTypeIds.Contains(x.Id) || !first)))
            .Select(x => _mapper.Map<EventTypeDto>(x))
            .ToList();
    }

    public async Task<Pagination<EventTypeDto>> Search(Guid activityId, SearchDto searchDto)
    {
        return _mappingHelper.ToDto(await _repository.Search(activityId, searchDto));
    }

    public async Task<ActivityEventType> Assign(EventTypeAssignDto dto)
    {
        var model = _mapper.Map<ActivityEventType>(dto);
        if (!await _activityEventTypeRepository.Validate(model))
            throw new Exception("Связь уже существует");

        return await _activityEventTypeRepository.AddEntity(model);
    }

    public async Task<bool> Delete(Guid activityId, Guid entityTypeId)
    {
        var links = await _activityEventTypeRepository
            .GetAll(
                x => x.ActivityId == activityId &&
                     x.EventTypeId == entityTypeId
            );

        return await _activityEventTypeRepository.DeleteBatch(links);
    }
}
