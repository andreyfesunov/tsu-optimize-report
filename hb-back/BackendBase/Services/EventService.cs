using AutoMapper;
using BackendBase.Dto;
using BackendBase.Dto.Event;
using BackendBase.Helpers;
using BackendBase.Interfaces.Services;
using BackendBase.Models;
using BackendBase.Repositories;

namespace BackendBase.Services;

public class EventService : IEventService
{
    private readonly IMapper _mapper;
    private readonly IEventRepository _repository;

    public EventService(IEventRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Event> AddEntity(Event entity)
    {
        return await _repository.AddEntity(entity);
    }

    public async Task<EventDto> GetById(Guid id)
    {
        return _mapper.Map<EventDto>(await _repository.GetById(id));
    }

    public async Task<ICollection<EventDto>> GetAll()
    {
        return (await _repository.GetAll()).Select(u => _mapper.Map<EventDto>(u)).ToList();
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
        var result = await _repository.Search(searchDto);
        return new Pagination<EventDto>
        {
            PageNumber = result.PageNumber,
            PageSize = result.PageSize,
            TotalPages = result.TotalPages,
            Entities = result.Entities.Select(u => _mapper.Map<EventDto>(u)).ToList()
        };
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