using Tsu.IndividualPlan.Domain.Dto.Event;
using Tsu.IndividualPlan.Domain.Interfaces.Repositories;
using Tsu.IndividualPlan.Domain.Interfaces.SecurityServices;
using Tsu.IndividualPlan.Domain.Interfaces.Services;
using Tsu.IndividualPlan.Domain.Models.Business;

namespace Tsu.IndividualPlan.Domain.Services;

public class EventService(IEventRepository repository, IEventSecurityService security)
    : IEventService
{
    public async Task<Event> Update(EventUpdateDto dto)
    {
        var entity = await repository.GetById(dto.Id);
        await security.validateCanUse(entity);
        // ****

        entity.StartedAt = dto.StartedAt;
        entity.EndedAt = dto.EndedAt;

        return await repository.UpdateEntity(entity);
    }

    public async Task<Event> AddEntity(EventCreateDto dto)
    {
        var entity = new Event(
            dto.StateUserId,
            dto.EventTypeId,
            dto.StartedAt,
            dto.EndedAt
        );

        await security.validateCanUse(entity);
        await security.validateCanCreate(entity);
        // ****

        return await repository.AddEntity(entity);
    }

    public async Task<bool> DeleteById(Guid id)
    {
        var entity = await repository.GetById(id);
        await security.validateCanUse(entity);
        // ****

        return await repository.Delete(entity);
    }
}