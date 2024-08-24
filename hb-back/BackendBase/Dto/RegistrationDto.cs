namespace BackendBase.Dto;

public class RegistrationDto
{
    public RegistrationDto(string Email, string Password, string Firstname, string Lastname)
    {
        this.Email = Email;
        this.Password = Password;
        this.Firstname = Firstname;
        this.Lastname = Lastname;
    }

    public string Email { get; init; }
    public string Password { get; init; }
    public string Firstname { get; init; }
    public string Lastname { get; init; }
}
