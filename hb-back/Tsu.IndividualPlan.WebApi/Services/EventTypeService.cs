using Tsu.IndividualPlan.WebApi.Dto;
using Tsu.IndividualPlan.WebApi.Exceptions;
using Tsu.IndividualPlan.WebApi.Extensions.Entities;
using Tsu.IndividualPlan.WebApi.Interfaces.Repositories;
using Tsu.IndividualPlan.WebApi.Interfaces.Services;
using Tsu.IndividualPlan.WebApi.Models;

namespace Tsu.IndividualPlan.WebApi.Services;

public class EventTypeService : IEventTypeService
{
    private readonly IActivityEventTypeRepository _activityEventTypeRepository;
    private readonly IActivityRepository _activityRepository;
    private readonly IEventTypeRepository _repository;
    private readonly IStateUserRepository _stateUserRepository;

    public EventTypeService(
        IEventTypeRepository repository,
        IActivityEventTypeRepository activityEventTypeRepository,
        IActivityRepository activityRepository,
        IStateUserRepository stateUserRepository
    )
    {
        _repository = repository;
        _activityEventTypeRepository = activityEventTypeRepository;
        _activityRepository = activityRepository;
        _stateUserRepository = stateUserRepository;
    }

    public async Task<ICollection<EventType>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<Dictionary<string, Pagination<EventTypeDto>>> SearchMap(SearchDto searchDto)
    {
        var searchMap = new Dictionary<string, Pagination<EventTypeDto>>();

        var activities = await _activityRepository.GetAll();

        foreach (var activity in activities)
        {
            var page = await Search(activity.Id, searchDto);
            searchMap[activity.Id.ToString()] = new Pagination<EventTypeDto>(
                PageSize: page.PageSize,
                PageNumber: page.PageNumber,
                TotalPages: page.TotalPages,
                Entities: page.Entities.Select(x => x.toDTO()).ToList()
            );
        }

        return searchMap;
    }

    public async Task<ICollection<EventType>> GetAllForReport(Guid stateUserId, Guid workId)
    {
        var stateUser = await _stateUserRepository.GetById(stateUserId);
        if (stateUser.Records == null) throw new AppException("Records not loaded");

        var activityIds = stateUser.Records.Select(x => x.Activity?.Id);
        var eventTypeIds = (
            await _activityEventTypeRepository.GetAll(x => activityIds.Contains(x.ActivityId))
        ).Select(x => x.EventTypeId);

        return await _repository.GetAll(x =>
            x.WorkId == workId
            && (
                eventTypeIds.Contains(x.Id)
                || workId != Guid.Parse(SystemWorks.AcademicMethodicalWorkId)
            )
        );
    }

    public async Task<Pagination<EventType>> Search(Guid activityId, SearchDto searchDto)
    {
        return await _repository.Search(activityId, searchDto);
    }

    public async Task<ActivityEventType> Assign(EventTypeAssignDto dto)
    {
        var model = new ActivityEventType(EventTypeId: dto.EventTypeId, ActivityId: dto.ActivityId);
        if (!await _activityEventTypeRepository.Validate(model))
            throw new Exception("Связь уже существует");

        return await _activityEventTypeRepository.AddEntity(model);
    }
}