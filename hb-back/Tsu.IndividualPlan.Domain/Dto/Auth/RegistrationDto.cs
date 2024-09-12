namespace Tsu.IndividualPlan.Domain.Dto.Auth;

public class RegistrationDto(string Email, string Password, string Firstname, string Lastname)
{
    public string Email { get; init; } = Email;
    public string Password { get; init; } = Password;
    public string Firstname { get; init; } = Firstname;
    public string Lastname { get; init; } = Lastname;
}