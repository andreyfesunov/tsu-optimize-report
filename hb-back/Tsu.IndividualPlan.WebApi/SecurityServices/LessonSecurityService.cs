using Tsu.IndividualPlan.WebApi.Dto;
using Tsu.IndividualPlan.WebApi.Exceptions;
using Tsu.IndividualPlan.WebApi.Interfaces.Repositories;
using Tsu.IndividualPlan.WebApi.Interfaces.SecurityServices;
using Tsu.IndividualPlan.WebApi.Models;

namespace Tsu.IndividualPlan.WebApi.SecurityServices;

public class LessonSecurityService : BaseSecurityService<Lesson>, ILessonSecurityService
{
    private readonly IEventRepository _eventRepository;
    private readonly IStateUserRepository _reportRepository;
    private readonly UserInfo _userInfo;

    public LessonSecurityService(
        UserInfo userInfo,
        IStateUserRepository reportRepository,
        IEventRepository eventRepository
    )
    {
        _userInfo = userInfo;
        _reportRepository = reportRepository;
        _eventRepository = eventRepository;
    }

    public override async Task validateCanUse(Lesson item)
    {
        // TODO n+1 problem
        var planEvent = await _eventRepository.GetById(item.EventId);
        var report = await _reportRepository.GetById(planEvent.StateUserId);

        if (report.UserId.ToString() != _userInfo.GetUserId()) throw new AppException("Can't use entity");
    }

    public async Task validateCanCreate(Lesson item)
    {
        var planEvent = await _eventRepository.GetById(item.EventId);

        if (planEvent.WorkId.ToString() != SystemWorks.AcademicMethodicalWorkId)
            throw new AppException("Lesson can only be created within Academical Methodical work");
        if (planEvent.HasLessonType(item.LessonTypeId)) throw new AppException("Lesson with given type already exists");
    }
}