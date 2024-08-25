using Tsu.IndividualPlan.WebApi.Dto.Event;
using Tsu.IndividualPlan.WebApi.Interfaces.Repositories;
using Tsu.IndividualPlan.WebApi.Interfaces.SecurityServices;
using Tsu.IndividualPlan.WebApi.Interfaces.Services;
using Tsu.IndividualPlan.WebApi.Models;

namespace Tsu.IndividualPlan.WebApi.Services;

public class EventService : IEventService
{
    private readonly IEventRepository _repository;
    private readonly IEventSecurityService _security;

    public EventService(IEventRepository repository, IEventSecurityService security)
    {
        _repository = repository;
        _security = security;
    }

    public async Task<Event> Update(EventUpdateDto dto)
    {
        var entity = await _repository.GetById(dto.Id);
        await _security.validateCanUse(entity);
        // ****

        entity.StartedAt = dto.StartedAt;
        entity.EndedAt = dto.EndedAt;

        return await _repository.UpdateEntity(entity);
    }

    public async Task<Event> AddEntity(EventCreateDto dto)
    {
        var entity = new Event(
            dto.StateUserId,
            dto.EventTypeId,
            dto.StartedAt,
            dto.EndedAt
        );

        await _security.validateCanUse(entity);
        await _security.validateCanCreate(entity);
        // ****

        return await _repository.AddEntity(entity);
    }

    public async Task<bool> DeleteById(Guid id)
    {
        var entity = await _repository.GetById(id);
        await _security.validateCanUse(entity);
        // ****

        return await _repository.Delete(entity);
    }
}