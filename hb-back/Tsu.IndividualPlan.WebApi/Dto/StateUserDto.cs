using Tsu.IndividualPlan.Domain.Enumerations;

namespace Tsu.IndividualPlan.WebApi.Dto;

public class StateUserDto(
    Guid Id,
    double Rate,
    StateUserStatus Status,
    StateDto? State,
    UserDto? User)
{
    public Guid Id { get; init; } = Id;
    public double Rate { get; init; } = Rate;
    public StateUserStatus Status { get; init; } = Status;
    public StateDto? State { get; init; } = State;
    public UserDto? User { get; init; } = User;
}