using BackendBase.Data.Dto;
using BackendBase.Models;

namespace BackendBase.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetAll();
        Task<User> GetById(string id);
        Task<TokenDto> Login(LoginDto loginDto);
        Task<bool> Registrate(RegistrationDto registrationDto);
    }
}
