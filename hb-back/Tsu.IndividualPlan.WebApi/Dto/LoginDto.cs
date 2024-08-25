namespace Tsu.IndividualPlan.WebApi.Dto;

public class LoginDto
{
    public LoginDto(string Email, string Password)
    {
        this.Email = Email;
        this.Password = Password;
    }

    public string Email { get; init; }
    public string Password { get; init; }
}