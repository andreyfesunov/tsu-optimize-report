using AutoMapper;
using BackendBase.Dto;
using BackendBase.Dto.Event;
using BackendBase.Helpers;
using BackendBase.Interfaces.Repositories.Common;
using BackendBase.Interfaces.Services;
using BackendBase.Models;
using BackendBase.Repositories;

namespace BackendBase.Services;

public class EventService : IEventService
{
    private readonly IMapper _mapper;
    private readonly MappingHelper<Event, EventDto> _mappingHelper;
    private readonly IBaseRepository<Event> _repository;

    public EventService(EventRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
        _mappingHelper = new MappingHelper<Event, EventDto>(_mapper);
    }

    public async Task<Event> AddEntity(Event entity)
    {
        return await _repository.AddEntity(entity);
    }

    public async Task<EventDto> GetById(Guid id)
    {
        return _mappingHelper.ToDto(await _repository.GetById(id));
    }

    public async Task<ICollection<EventDto>> GetAll()
    {
        return _mappingHelper.ToDto(await _repository.GetAll());
    }

    public async Task<Event> Update(Event entity)
    {
        return await _repository.UpdateEntity(entity);
    }

    public async Task<bool> DeleteById(Guid entityId)
    {
        return await _repository.DeleteById(entityId);
    }

    public async Task<Pagination<EventDto>> Search(SearchDto searchDto)
    {
        return _mappingHelper.ToDto(await _repository.Search(searchDto));
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