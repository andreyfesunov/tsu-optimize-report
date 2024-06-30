using BackendBase.Dto;
using BackendBase.Models;
using MathNet.Numerics.Statistics.Mcmc;

namespace BackendBase.Interfaces;

public interface IEventTypeService
{
    Task<PaginationDto<EventTypeDto>> Search(Guid activityId, SearchDto searchDto);
    Task<ActivityEventType> Assign(EventTypeAssignDto dto);
    Task<Dictionary<string, PaginationDto<EventTypeDto>>> SearchMap(SearchDto searchDto);
    Task<ICollection<EventTypeDto>> GetAllForReport(Guid stateUserId, Guid workId, bool first);
    Task<bool> Delete(Guid activityId, Guid entityTypeId);
    Task<EventType> AddEntity(EventType entity);
    Task<EventTypeDto> GetById(Guid id);
    Task<ICollection<EventTypeDto>> GetAll();
    Task<EventType> Update(EventType entity);
    Task<bool> DeleteById(Guid entityId);
    Task<PaginationDto<EventTypeDto>> Search(SearchDto searchDto);
}