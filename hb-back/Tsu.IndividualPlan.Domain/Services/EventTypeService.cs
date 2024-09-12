using Tsu.IndividualPlan.Domain.Dto.EventType;
using Tsu.IndividualPlan.Domain.Exceptions;
using Tsu.IndividualPlan.Domain.Interfaces.Repositories;
using Tsu.IndividualPlan.Domain.Interfaces.Services;
using Tsu.IndividualPlan.Domain.Models.Business;
using Tsu.IndividualPlan.Domain.Models.Project;

namespace Tsu.IndividualPlan.Domain.Services;

public class EventTypeService(
    IEventTypeRepository repository,
    IActivityEventTypeRepository activityEventTypeRepository,
    IActivityRepository activityRepository,
    IStateUserRepository stateUserRepository)
    : IEventTypeService
{
    public async Task<ICollection<EventType>> GetAll()
    {
        return await repository.GetAll();
    }

    public async Task<Dictionary<string, Pagination<EventType>>> SearchMap(Search search)
    {
        var searchMap = new Dictionary<string, Pagination<EventType>>();

        var activities = await activityRepository.GetAll();

        foreach (var activity in activities)
            searchMap[activity.Id.ToString()] = await Search(activity.Id, search);

        return searchMap;
    }

    public async Task<ICollection<EventType>> GetAllForReport(Guid stateUserId, Guid workId)
    {
        var stateUser = await stateUserRepository.GetById(stateUserId);
        if (stateUser.Records == null) throw new AppException("Records not loaded");

        var activityIds = stateUser.Records.Select(x => x.Activity?.Id);
        var eventTypeIds = (
            await activityEventTypeRepository.GetAll(x => activityIds.Contains(x.ActivityId))
        ).Select(x => x.EventTypeId);

        return await repository.GetAll(x =>
            x.WorkId == workId
            && (
                eventTypeIds.Contains(x.Id)
                || workId != Guid.Parse(SystemWorks.AcademicMethodicalWorkId)
            )
        );
    }

    public async Task<Pagination<EventType>> Search(Guid activityId, Search search)
    {
        return await repository.Search(activityId, search);
    }

    public async Task<ActivityEventType> Assign(EventTypeAssignDto dto)
    {
        var model = new ActivityEventType(EventTypeId: dto.EventTypeId, ActivityId: dto.ActivityId);
        if (!await activityEventTypeRepository.Validate(model))
            throw new Exception("Связь уже существует");

        return await activityEventTypeRepository.AddEntity(model);
    }
}