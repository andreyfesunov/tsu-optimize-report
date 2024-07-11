using BackendBase.Interfaces.Repositories.Common;
using BackendBase.Models;

namespace BackendBase.Interfaces.Repositories;

public interface IStateRepository : IGetRepository<State>, ISearchRepository<State>, ICreateRepository<State>, IUpdateRepository<State>
{
}
