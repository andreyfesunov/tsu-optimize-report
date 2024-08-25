using BackendBase.Interfaces.Repositories;
using BackendBase.Interfaces.Services;
using BackendBase.Models;

namespace BackendBase.Services;

public class WorkService : IWorkService
{
    protected readonly IWorkRepository _repository;

    public WorkService(IWorkRepository repository)
    {
        _repository = repository;
    }

    public async Task<ICollection<Work>> GetAll()
    {
        return await _repository.GetAll();
    }
}
