using BackendBase.Dto;
using BackendBase.Helpers.CRUD;
using BackendBase.Models;
using BackendBase.Models.Enum;

namespace BackendBase.Interfaces
{
    public interface IUserService : ICRUDServiceBase<User>
    {
        Task<List<UserDto>> GetAllUsers();
        Task<UserDto> GetUserById(Guid userId);
        Task<UserLoginDto> LogIn(LoginDto loginDto);
        Task<RoleUserEnum> Reg(RegistrationDto registrationDto);
    }
}
