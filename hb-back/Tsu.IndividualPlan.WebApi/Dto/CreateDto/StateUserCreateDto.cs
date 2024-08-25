namespace Tsu.IndividualPlan.WebApi.Dto.CreateDto;

public class StateUserCreateDto
{
    public StateUserCreateDto(Guid StateId, Guid UserId)
    {
        this.StateId = StateId;
        this.UserId = UserId;
    }

    public Guid StateId { get; init; }
    public Guid UserId { get; init; }
}