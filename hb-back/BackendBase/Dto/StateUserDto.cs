using BackendBase.Models.Enum;

namespace BackendBase.Dto;

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

    public readonly Guid Id;
    public readonly double Rate;
    public readonly StateUserStatus Status;
    public readonly StateDto? State;
    public readonly UserDto? User;
}
