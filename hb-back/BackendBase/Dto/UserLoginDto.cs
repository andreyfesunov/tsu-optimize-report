namespace BackendBase.Dto;

public class UserLoginDto
{
    public UserLoginDto(string Token) => this.Token = Token;

    public string Token { get; init; }
}
