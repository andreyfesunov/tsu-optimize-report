using AutoMapper;
using BackendBase.Dto;
using BackendBase.Dto.Event;
using BackendBase.Helpers.CRUD;
using BackendBase.Interfaces;
using BackendBase.Models;
using BackendBase.Repositories;

namespace BackendBase.Services;

public class EventService : CRUDServiceBase<Event, EventDto>, IEventService
{
    public EventService(EventRepository repository, IMapper mapper) : base(mapper)
    {
        _repository = repository;
    }

    public async Task<Event> Update(EventUpdateDto entity)
    {
        var eventEntity = await _repository.GetById(Guid.Parse(entity.Id));

        eventEntity.StartedAt = entity.StartedAt;
        eventEntity.EndedAt = entity.EndedAt;

        await _repository.UpdateEntity(eventEntity);

        return eventEntity;
    }
}