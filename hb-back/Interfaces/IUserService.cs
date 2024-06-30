using BackendBase.Dto;
using BackendBase.Models;
using BackendBase.Models.Enum;

namespace BackendBase.Interfaces
{
    public interface IUserService
    {
        Task<User> AddEntity(User entity);
        Task<UserDto> GetById(Guid id);
        Task<ICollection<UserDto>> GetAll();
        Task<User> Update(User entity);
        Task<bool> DeleteById(Guid entityId);
        Task<PaginationDto<UserDto>> Search(SearchDto searchDto);
        Task<List<UserDto>> GetAllUsers();
        Task<UserDto> GetUserById(Guid userId);
        Task<UserLoginDto> LogIn(LoginDto loginDto);
        Task<RoleUserEnum> Reg(RegistrationDto registrationDto);
    }
}
