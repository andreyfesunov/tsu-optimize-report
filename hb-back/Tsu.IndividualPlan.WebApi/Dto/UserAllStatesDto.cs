namespace Tsu.IndividualPlan.WebApi.Dto;

public class UserAllStatesDto(
    UserDto? User,
    IEnumerable<StateDto>? States)
{
    public UserDto User { get; init; } = User;
    public IEnumerable<StateDto> States { get; init; } = States;
}