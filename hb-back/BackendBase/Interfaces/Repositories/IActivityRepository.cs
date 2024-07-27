using BackendBase.Models;

namespace BackendBase.Repositories
{
    public interface IActivityRepository
    {
        Task<ICollection<Activity>> GetAll();
    }
}
