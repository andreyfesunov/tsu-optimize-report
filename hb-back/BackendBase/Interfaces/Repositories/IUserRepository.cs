using BackendBase.Interfaces.Repositories.Common;
using BackendBase.Models;

namespace BackendBase.Interfaces.Repositories;

public interface IUserRepository : IGetRepository<User>, ISearchRepository<User>, ICreateRepository<User>
{
    public Task<User?> GetByEmail(string email);
}