namespace Tsu.IndividualPlan.WebApi.Dto;

public class UserLoginDto(string Token)
{
    public string Token { get; init; } = Token;
}