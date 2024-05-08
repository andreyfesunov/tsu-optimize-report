using BackendBase.Dto;
using BackendBase.Models.Enum;

namespace BackendBase.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUsers();
        Task<UserDto> GetUserById(Guid userId);
        Task<UserLoginDto> LogIn(LoginDto loginDto);
        Task<RoleUserEnum> Reg(RegistrationDto registrationDto);
    }
}
