namespace Tsu.IndividualPlan.Domain.Dto.Auth;

public class LoginDto(string Email, string Password)
{
    public string Email { get; init; } = Email;
    public string Password { get; init; } = Password;
}