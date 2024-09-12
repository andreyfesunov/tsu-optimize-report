using Tsu.IndividualPlan.Domain.Dto.EventType;
using Tsu.IndividualPlan.Domain.Models.Business;
using Tsu.IndividualPlan.Domain.Models.Project;

namespace Tsu.IndividualPlan.Domain.Interfaces.Services;

public interface IEventTypeService
{
    Task<Pagination<EventType>> Search(Guid activityId, Search search);
    Task<ActivityEventType> Assign(EventTypeAssignDto dto);
    Task<Dictionary<string, Pagination<EventType>>> SearchMap(Search search);
    Task<ICollection<EventType>> GetAllForReport(Guid stateUserId, Guid workId);
    Task<ICollection<EventType>> GetAll();
}