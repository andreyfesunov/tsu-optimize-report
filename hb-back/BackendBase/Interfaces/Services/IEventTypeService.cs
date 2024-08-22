using BackendBase.Dto;
using BackendBase.Models;

namespace BackendBase.Interfaces.Services;

public interface IEventTypeService
{
    Task<Pagination<EventTypeDto>> Search(Guid activityId, SearchDto searchDto);
    Task<ActivityEventType> Assign(EventTypeAssignDto dto);
    Task<Dictionary<string, Pagination<EventTypeDto>>> SearchMap(SearchDto searchDto);
    Task<ICollection<EventTypeDto>> GetAllForReport(Guid stateUserId, Guid workId, bool first);
    Task<bool> Delete(Guid activityId, Guid entityTypeId);
    Task<ICollection<EventTypeDto>> GetAll();
    Task<EventType> Update(EventType entity);
    Task<bool> DeleteById(Guid entityId);

    Task<Pagination<EventType>> Search(SearchDto searchDto);
}
