using Tsu.IndividualPlan.WebApi.Interfaces.Repositories;
using Tsu.IndividualPlan.WebApi.Interfaces.Services;
using Tsu.IndividualPlan.WebApi.Models;

namespace Tsu.IndividualPlan.WebApi.Services;

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