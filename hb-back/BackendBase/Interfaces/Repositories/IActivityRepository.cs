using BackendBase.Models;

namespace BackendBase.Interfaces.Repositories
{
    public interface IActivityRepository
    {
        Task<ICollection<Activity>> GetAll();
    }
}
