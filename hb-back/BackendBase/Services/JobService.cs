using BackendBase.Interfaces.Repositories;
using BackendBase.Interfaces.Services;
using BackendBase.Models;

namespace BackendBase.Services;

public class JobService : IJobService
{
    protected IJobRepository _repository;

    public JobService(IJobRepository repository)
    {
        _repository = repository;
    }

    public async Task<ICollection<Job>> GetAll()
    {
        return await _repository.GetAll();
    }
}
