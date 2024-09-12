using Tsu.IndividualPlan.Domain.Exceptions;
using Tsu.IndividualPlan.Domain.Interfaces.Repositories;
using Tsu.IndividualPlan.Domain.Interfaces.SecurityServices;
using Tsu.IndividualPlan.Domain.Models.Business;
using Tsu.IndividualPlan.Domain.Models.Project;

namespace Tsu.IndividualPlan.Domain.Security;

public class EventSecurityService : BaseSecurityService<Event>, IEventSecurityService
{
    private readonly IStateUserRepository _repository;
    private readonly UserInfo _userInfo;

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

        if (stateUser.UserId.ToString() != _userInfo.GetUserId()) throw new AppException("Can't use entity");
    }

    public async Task validateCanCreate(Event item)
    {
        var stateUser = await _repository.GetById(item.StateUserId);

        if (stateUser.Events == null)
            throw new AppException("Events not loaded");

        if (stateUser.Events.Any(ev => ev.EventTypeId == item.EventTypeId))
            throw new AppException("Event with given type already exists");
    }
}