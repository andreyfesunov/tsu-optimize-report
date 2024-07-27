using BackendBase.Models;

namespace BackendBase.Interfaces.Services;

public interface IActivityService
{
    Task<ICollection<Activity>> GetAll();
}
