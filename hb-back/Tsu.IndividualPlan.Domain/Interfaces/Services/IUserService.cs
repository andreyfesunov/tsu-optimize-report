using Tsu.IndividualPlan.Domain.Dto.Auth;
using Tsu.IndividualPlan.Domain.Enumerations;
using Tsu.IndividualPlan.Domain.Models.Business;
using Tsu.IndividualPlan.Domain.Models.Project;

namespace Tsu.IndividualPlan.Domain.Interfaces.Services;

public interface IUserService
{
    Task<string> LogIn(LoginDto loginDto);
    Task<RoleUserEnum> Reg(RegistrationDto registrationDto);
    Task<User> GetById(Guid id);
    Task<ICollection<User>> GetAll();
    Task<Pagination<User>> Search(Search search);
}