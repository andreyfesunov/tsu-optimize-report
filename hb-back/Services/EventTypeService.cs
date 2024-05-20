using AutoMapper;
using BackendBase.Dto;
using BackendBase.Helpers.CRUD;
using BackendBase.Interfaces;
using BackendBase.Models;
using BackendBase.Repositories;

namespace BackendBase.Services;

public class EventTypeService : CRUDServiceBase<EventType, EventTypeDto>, IEventTypeService
{
    private readonly ActivityEventTypeRepository _activityEventTypeRepository;
    private readonly ActivityRepository _activityRepository;
    private readonly EventTypeRepository _eventRepository;
    private readonly IMapper _mapper;
    private readonly StateUserRepository _stateUserRepository;

    public EventTypeService(
        EventTypeRepository repository,
        ActivityEventTypeRepository activityEventTypeRepository,
        ActivityRepository activityRepository,
        StateUserRepository stateUserRepository,
        IMapper mapper
    ) : base(mapper)
    {
        _repository = repository;
        _eventRepository = repository;
        _activityEventTypeRepository = activityEventTypeRepository;
        _activityRepository = activityRepository;
        _stateUserRepository = stateUserRepository;
        _mapper = mapper;
    }

    public async Task<Dictionary<string, PaginationDto<EventTypeDto>>> SearchMap(SearchDto searchDto)
    {
        var searchMap = new Dictionary<string, PaginationDto<EventTypeDto>>();

        var activities = await _activityRepository.GetAll();

        foreach (var activity in activities)
            searchMap[activity.Id.ToString()] = await Search(activity.Id, searchDto);

        return searchMap;
    }

    public async Task<ICollection<EventTypeDto>> GetAllForReport(Guid stateUserId, Guid workId)
    {
        var stateUser = await _stateUserRepository.GetById(stateUserId);
        var activityIds = stateUser.Records.Select(x => x.Activity.Id);
        var eventTypeIds =
            (await _activityEventTypeRepository.GetAll(x => activityIds.Contains(x.ActivityId))).Select(x =>
                x.EventTypeId);

        return (await _eventRepository.GetAll(x => x.WorkId == workId && eventTypeIds.Contains(x.Id)))
            .Select(x => _mapper.Map<EventTypeDto>(x))
            .ToList();
    }

    public async Task<PaginationDto<EventTypeDto>> Search(Guid activityId, SearchDto searchDto)
    {
        return _mappingHelper.paginationToDto(await _eventRepository.Search(activityId, searchDto));
    }

    public async Task<ActivityEventType> Assign(EventTypeAssignDto dto)
    {
        var model = _mapper.Map<ActivityEventType>(dto);
        if (!await _activityEventTypeRepository.Validate(model))
            throw new Exception("Связь уже существует");

        return await _activityEventTypeRepository.AddEntity(model);
    }
}