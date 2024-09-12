namespace Tsu.IndividualPlan.Domain.Dto.IndividualPlan;

public class IndividualPlanCreateDto(Guid StateId, Guid UserId)
{
    public Guid StateId { get; init; } = StateId;
    public Guid UserId { get; init; } = UserId;
}