using BackendBase.Dto;
using BackendBase.Exceptions;
using BackendBase.Interfaces.Repositories;
using BackendBase.Interfaces.SecurityServices;
using BackendBase.Models;

namespace BackendBase.SecurityServices;

public class CommentSecurityService : BaseSecurityService<Comment>, ICommentSecurityService
{
    private readonly UserInfo _userInfo;
    private readonly IStateUserRepository _reportRepository;
    private readonly IEventRepository _eventRepository;

    public CommentSecurityService(
        UserInfo userInfo,
        IStateUserRepository reportRepository,
        IEventRepository eventRepository
    )
    {
        _userInfo = userInfo;
        _reportRepository = reportRepository;
        _eventRepository = eventRepository;
    }

    public override async Task validateCanUse(Comment item)
    {
        // TODO n+1 problem
        var @event = await _eventRepository.GetById(item.EventId);
        var report = await _reportRepository.GetById(@event.StateUserId);

        if (report.UserId.ToString() != _userInfo.GetUserId())
        {
            throw new AppException("Can't use entity");
        }
    }

    public async Task validateCanCreate(Comment item)
    {
        var @event = await _eventRepository.GetById(item.EventId);

        if (@event.WorkId.ToString() == SystemWorks.AcademicMethodicalWorkId)
        {
            throw new AppException("Comment cannot be created inside Academical Methodical Work");
        }
    }
}
