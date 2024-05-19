using BackendBase.Dto;
using BackendBase.Helpers.CRUD;
using BackendBase.Models;

namespace BackendBase.Interfaces;

public interface IEventTypeService : ICRUDServiceBase<EventType, EventTypeDto>
{
    Task<PaginationDto<EventTypeDto>> Search(Guid activityId, SearchDto searchDto);
    Task<ActivityEventType> Assign(EventTypeAssignDto dto);
}