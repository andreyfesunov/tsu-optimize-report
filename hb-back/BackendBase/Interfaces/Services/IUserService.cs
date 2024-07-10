using BackendBase.Dto;
using BackendBase.Models.Enum;

namespace BackendBase.Interfaces.Services;

public interface IUserService
{
    Task<string> LogIn(LoginDto loginDto);
    Task<RoleUserEnum> Reg(RegistrationDto registrationDto);
}