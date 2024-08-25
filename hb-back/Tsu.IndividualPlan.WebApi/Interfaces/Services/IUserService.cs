using Tsu.IndividualPlan.WebApi.Dto;
using Tsu.IndividualPlan.WebApi.Models;
using Tsu.IndividualPlan.WebApi.Models.Enum;

namespace Tsu.IndividualPlan.WebApi.Interfaces.Services;

public interface IUserService
{
    Task<string> LogIn(LoginDto loginDto);
    Task<RoleUserEnum> Reg(RegistrationDto registrationDto);
    Task<User> GetById(Guid id);
    Task<ICollection<User>> GetAll();
    Task<Pagination<User>> Search(SearchDto searchDto);
}