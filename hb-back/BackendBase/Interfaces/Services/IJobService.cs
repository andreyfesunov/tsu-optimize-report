using BackendBase.Models;

namespace BackendBase.Interfaces.Services;

public interface IJobService
{
    Task<ICollection<Job>> GetAll();
}
