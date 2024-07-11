using BackendBase.Interfaces.Repositories.Common;
using BackendBase.Models;

namespace BackendBase.Interfaces.Repositories;

public interface IStateUserRepository : IGetRepository<StateUser>, ICreateRepository<StateUser>, ISearchRepository<StateUser> {
}
