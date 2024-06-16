using BackendBase.Dto;
using BackendBase.Helpers.CRUD;
using BackendBase.Models;

namespace BackendBase.Interfaces;

public interface IEventTypeService : ICRUDServiceBase<EventType, EventTypeDto>
{
    Task<PaginationDto<EventTypeDto>> Search(Guid activityId, SearchDto searchDto);
    Task<ActivityEventType> Assign(EventTypeAssignDto dto);
    Task<Dictionary<string, PaginationDto<EventTypeDto>>> SearchMap(SearchDto searchDto);
    Task<ICollection<EventTypeDto>> GetAllForReport(Guid stateUserId, Guid workId, bool first);
}