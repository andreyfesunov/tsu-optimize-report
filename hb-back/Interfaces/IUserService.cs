using BackendBase.Dto;

namespace BackendBase.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUsers();
        Task<UserDto> GetUserById(Guid userId);
        Task<UserLoginDto> LogIn(LoginDto loginDto);
        Task<string> Reg(RegistrationDto registrationDto);
    }
}
