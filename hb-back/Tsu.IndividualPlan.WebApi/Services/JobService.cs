using Tsu.IndividualPlan.WebApi.Interfaces.Repositories;
using Tsu.IndividualPlan.WebApi.Interfaces.Services;
using Tsu.IndividualPlan.WebApi.Models;

namespace Tsu.IndividualPlan.WebApi.Services;

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