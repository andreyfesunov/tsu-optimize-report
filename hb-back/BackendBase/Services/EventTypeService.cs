using AutoMapper;
using BackendBase.Dto;
using BackendBase.Interfaces.Repositories;
using BackendBase.Interfaces.Services;
using BackendBase.Models;

namespace BackendBase.Services;

public class EventTypeService : IEventTypeService
{
    private readonly IActivityEventTypeRepository _activityEventTypeRepository;
    private readonly IActivityRepository _activityRepository;
    private readonly IMapper _mapper;
    private readonly IEventTypeRepository _repository;
    private readonly IStateUserRepository _stateUserRepository;

    public EventTypeService(
        IEventTypeRepository repository,
        IActivityEventTypeRepository activityEventTypeRepository,
        IActivityRepository activityRepository,
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

    public async Task<ICollection<EventTypeDto>> GetAll()
    {
        return (await _repository.GetAll()).Select(u => _mapper.Map<EventTypeDto>(u)).ToList();
    }

    public async Task<EventType> Update(EventType entity)
    {
        return await _repository.UpdateEntity(entity);
    }

    public async Task<bool> DeleteById(Guid entityId)
    {
        return await _repository.DeleteById(entityId);
    }

    public async Task<Pagination<EventType>> Search(SearchDto searchDto) =>
        await _repository.Search(searchDto);

    public async Task<Dictionary<string, Pagination<EventTypeDto>>> SearchMap(SearchDto searchDto)
    {
        var searchMap = new Dictionary<string, Pagination<EventTypeDto>>();

        var activities = await _activityRepository.GetAll();

        foreach (var activity in activities)
            searchMap[activity.Id.ToString()] = await Search(activity.Id, searchDto);

        return searchMap;
    }

    public async Task<ICollection<EventTypeDto>> GetAllForReport(
        Guid stateUserId,
        Guid workId,
        bool first
    )
    {
        var stateUser = await _stateUserRepository.GetById(stateUserId);
        var activityIds = stateUser.Records.Select(x => x.Activity?.Id);
        var eventTypeIds = (
            await _activityEventTypeRepository.GetAll(x => activityIds.Contains(x.ActivityId))
        ).Select(x => x.EventTypeId);

        return (
            await _repository.GetAll(x =>
                x.WorkId == workId && (eventTypeIds.Contains(x.Id) || !first)
            )
        )
            .Select(x => _mapper.Map<EventTypeDto>(x))
            .ToList();
    }

    public async Task<Pagination<EventTypeDto>> Search(Guid activityId, SearchDto searchDto)
    {
        var result = await _repository.Search(activityId, searchDto);
        return new Pagination<EventTypeDto>(
            PageNumber: result.PageNumber,
            PageSize: result.PageSize,
            TotalPages: result.TotalPages,
            Entities: result.Entities.Select(u => _mapper.Map<EventTypeDto>(u)).ToList()
        );
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
        var links = await _activityEventTypeRepository.GetAll(x =>
            x.ActivityId == activityId && x.EventTypeId == entityTypeId
        );

        return await _activityEventTypeRepository.DeleteBatch(links);
    }
}
