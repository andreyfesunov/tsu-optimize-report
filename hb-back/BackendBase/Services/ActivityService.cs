using BackendBase.Interfaces.Services;
using BackendBase.Models;
using BackendBase.Repositories;

namespace BackendBase.Services;

public class ActivityService : IActivityService
{
    private readonly IActivityRepository _repository;

    public ActivityService(IActivityRepository repository)
    {
        _repository = repository;
    }

    public Task<ICollection<Activity>> GetAll() => _repository.GetAll();
}
