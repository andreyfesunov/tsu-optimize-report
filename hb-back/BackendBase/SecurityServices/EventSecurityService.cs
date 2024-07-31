using BackendBase.Dto;
using BackendBase.Exceptions;
using BackendBase.Interfaces.Repositories;
using BackendBase.Interfaces.SecurityServices;
using BackendBase.Models;

namespace BackendBase.SecurityServices;

public class EventSecurityService : BaseSecurityService<Event>, IEventSecurityService
{
    private readonly UserInfo _userInfo;
    private readonly IStateUserRepository _repository;

    public EventSecurityService(
            UserInfo userInfo,
            IStateUserRepository repository
    )
    {
        _userInfo = userInfo;
        _repository = repository;
    }

    public override async Task validateCanUse(Event item)
    {
        var stateUser = await _repository.GetById(item.StateUserId);

        if (stateUser.UserId.ToString() != _userInfo.GetUserId())
        {
            throw new AppException("Can't use entity");
        }
    }

    public async Task validateCanCreate(Event item)
    {
        var stateUser = await _repository.GetById(item.StateUserId);

        if (stateUser.Events.Where(ev => ev.EventTypeId == item.EventTypeId).Count() != 0)
        {
            throw new AppException("Event with given type already exists");
        }
    }
}
