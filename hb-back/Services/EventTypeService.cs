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
    private readonly EventTypeRepository _eventRepository;
    private readonly IMapper _mapper;

    public EventTypeService(
        EventTypeRepository repository,
        ActivityEventTypeRepository activityEventTypeRepository,
        IMapper mapper
    )
    {
        _repository = repository;
        _eventRepository = repository;
        _activityEventTypeRepository = activityEventTypeRepository;
        _mapper = mapper;
    }

    public Task<PaginationDto<EventTypeDto>> Search(Guid activityId, SearchDto searchDto)
    {
        return _eventRepository.Search(activityId, searchDto);
    }

    public async Task<ActivityEventType> Assign(EventTypeAssignDto dto)
    {
        var model = _mapper.Map<ActivityEventType>(dto);
        if (!await _activityEventTypeRepository.Validate(model)) throw new Exception("Связь уже существует");

        return await _activityEventTypeRepository.AddEntity(model);
    }
}