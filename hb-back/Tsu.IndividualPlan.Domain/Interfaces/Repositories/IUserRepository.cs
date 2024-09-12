using Tsu.IndividualPlan.Domain.Models.Business;
using Tsu.IndividualPlan.Domain.Models.Project;

namespace Tsu.IndividualPlan.Domain.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User?> GetByEmail(string email);
    Task<User> GetById(Guid id);
    Task<ICollection<User>> GetAll();
    Task<Pagination<User>> Search(Search search);
    Task<User> AddEntity(User entity);
}