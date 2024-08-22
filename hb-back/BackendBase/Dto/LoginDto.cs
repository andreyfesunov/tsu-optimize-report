using System.ComponentModel.DataAnnotations;

namespace BackendBase.Dto;

public class LoginDto
{
    public LoginDto(string Email, string Password)
    {
        this.Email = Email;
        this.Password = Password;
    }

    [Required]
    public readonly string Email;

    [DataType(DataType.Password)]
    public readonly string Password;
}
