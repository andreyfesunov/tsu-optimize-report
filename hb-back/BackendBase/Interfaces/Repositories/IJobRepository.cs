using BackendBase.Models;

namespace BackendBase.Interfaces.Repositories;

public interface IJobRepository
{
    Task<ICollection<Job>> GetAll();
}
