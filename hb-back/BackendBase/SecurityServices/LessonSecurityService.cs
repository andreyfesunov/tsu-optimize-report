using BackendBase.Dto;
using BackendBase.Exceptions;
using BackendBase.Interfaces.Repositories;
using BackendBase.Interfaces.SecurityServices;
using BackendBase.Models;

namespace BackendBase.SecurityServices;

public class LessonSecurityService : BaseSecurityService<Lesson>, ILessonSecurityService
{
    private readonly UserInfo _userInfo;
    private readonly IStateUserRepository _reportRepository;
    private readonly IEventRepository _eventRepository;

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

        if (report.UserId.ToString() != _userInfo.GetUserId())
        {
            throw new AppException("Can't use entity");
        }
    }

    public async Task validateCanCreate(Lesson item)
    {
        var planEvent = await _eventRepository.GetById(item.EventId);

        if (planEvent.WorkId.ToString() != SystemWorks.AcademicMethodicalWorkId)
        {
            throw new AppException("Lesson can only be created within Academical Methodical work");
        }
        if (planEvent.HasLessonType(item.LessonTypeId))
        {
            throw new AppException("Lesson with given type already exists");
        }
    }
}
