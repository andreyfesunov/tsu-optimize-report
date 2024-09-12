using Tsu.IndividualPlan.Domain.Interfaces.Repositories;
using Tsu.IndividualPlan.Domain.Interfaces.Services;
using Tsu.IndividualPlan.Domain.Models.Business;

namespace Tsu.IndividualPlan.Domain.Services;

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