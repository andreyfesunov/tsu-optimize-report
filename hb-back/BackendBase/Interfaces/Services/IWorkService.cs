using BackendBase.Models;

namespace BackendBase.Interfaces.Services;

public interface IWorkService
{
    Task<ICollection<Work>> GetAll();
}
