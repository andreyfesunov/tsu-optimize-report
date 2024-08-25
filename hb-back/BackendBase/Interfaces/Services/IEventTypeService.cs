using BackendBase.Dto;
using BackendBase.Models;

namespace BackendBase.Interfaces.Services;

public interface IEventTypeService
{
    Task<Pagination<EventType>> Search(Guid activityId, SearchDto searchDto);
    Task<ActivityEventType> Assign(EventTypeAssignDto dto);
    Task<Dictionary<string, Pagination<EventTypeDto>>> SearchMap(SearchDto searchDto);
    Task<ICollection<EventType>> GetAllForReport(Guid stateUserId, Guid workId);
    Task<ICollection<EventType>> GetAll();
}
