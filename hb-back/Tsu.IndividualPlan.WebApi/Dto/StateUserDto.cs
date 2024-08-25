using Tsu.IndividualPlan.WebApi.Models.Enum;

namespace Tsu.IndividualPlan.WebApi.Dto;

public class StateUserDto
{
    public StateUserDto(
        Guid Id,
        double Rate,
        StateUserStatus Status,
        StateDto? State,
        UserDto? User
    )
    {
        this.Id = Id;
        this.Rate = Rate;
        this.Status = Status;
        this.State = State;
        this.User = User;
    }

    public Guid Id { get; init; }
    public double Rate { get; init; }
    public StateUserStatus Status { get; init; }
    public StateDto? State { get; init; }
    public UserDto? User { get; init; }
}