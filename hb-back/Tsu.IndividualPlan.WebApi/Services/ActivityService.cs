using Tsu.IndividualPlan.WebApi.Interfaces.Repositories;
using Tsu.IndividualPlan.WebApi.Interfaces.Services;
using Tsu.IndividualPlan.WebApi.Models;

namespace Tsu.IndividualPlan.WebApi.Services;

public class ActivityService : IActivityService
{
    private readonly IActivityRepository _repository;

    public ActivityService(IActivityRepository repository)
    {
        _repository = repository;
    }

    public Task<ICollection<Activity>> GetAll()
    {
        return _repository.GetAll();
    }
}