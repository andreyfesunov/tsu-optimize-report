using BackendBase.Dto;
using BackendBase.Helpers.CRUD;
using BackendBase.Interfaces;
using BackendBase.Models;
using BackendBase.Repositories;

namespace BackendBase.Services;

public class EventTypeService : CRUDServiceBase<EventType, EventTypeDto>, IEventTypeService
{
    private readonly EventTypeRepository _eventRepository;

    public EventTypeService(EventTypeRepository repository)
    {
        _repository = repository;
        _eventRepository = repository;
    }

    public Task<PaginationDto<EventTypeDto>> Search(Guid activityId, SearchDto searchDto)
    {
        return _eventRepository.Search(activityId, searchDto);
    }
}