using AutoMapper;
using BackendBase.Dto;
using BackendBase.Helpers;
using BackendBase.Helpers.CRUD;
using BackendBase.Interfaces;
using BackendBase.Models;
using BackendBase.Repositories;

namespace BackendBase.Services;

public class EventTypeService : CRUDServiceBase<EventType, EventTypeDto>, IEventTypeService
{
    private readonly ActivityEventTypeRepository _activityEventTypeRepository;
    private readonly EventTypeRepository _eventRepository;
    private readonly ActivityRepository _activityRepository;
    private readonly IMapper _mapper;

    public EventTypeService(
        EventTypeRepository repository,
        ActivityEventTypeRepository activityEventTypeRepository,
        ActivityRepository activityRepository,
        IMapper mapper
    ) : base(mapper)
    {
        _repository = repository;
        _eventRepository = repository;
        _activityEventTypeRepository = activityEventTypeRepository;
        _activityRepository = activityRepository;
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

    public async Task<PaginationDto<EventTypeDto>> Search(Guid activityId, SearchDto searchDto)
    {
        return MappingHelper<EventType, EventTypeDto>.paginationToDto((await _eventRepository.Search(activityId, searchDto)), _mapper);
    }

    public async Task<ActivityEventType> Assign(EventTypeAssignDto dto)
    {
        var model = _mapper.Map<ActivityEventType>(dto);
        if (!await _activityEventTypeRepository.Validate(model)) throw new Exception("Связь уже существует");

        return await _activityEventTypeRepository.AddEntity(model);
    }
}