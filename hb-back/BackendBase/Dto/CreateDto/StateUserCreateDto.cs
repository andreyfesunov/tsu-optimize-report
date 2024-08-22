namespace BackendBase.Dto.CreateDto;

public class StateUserCreateDto
{
    public StateUserCreateDto(Guid StateId, Guid UserId)
    {
        this.StateId = StateId;
        this.UserId = UserId;
    }

    public readonly Guid StateId;
    public readonly Guid UserId;
}
