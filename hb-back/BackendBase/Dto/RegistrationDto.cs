using System.ComponentModel.DataAnnotations;

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

    [Required]
    public readonly string Email;

    [Required]
    [DataType(DataType.Password)]
    public readonly string Password;

    [Required]
    public readonly string Firstname;

    [Required]
    public readonly string Lastname;
}
