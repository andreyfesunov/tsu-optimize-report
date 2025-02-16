using Tsu.IndividualPlan.Domain.Interfaces.Repositories;
using Tsu.IndividualPlan.Domain.Interfaces.Services;
using Tsu.IndividualPlan.Domain.Models.Business;

namespace Tsu.IndividualPlan.Domain.Services;

public class DepartmentService(
    IDepartmentRepository repository)
    : IDepartmentService
{
    public Task<ICollection<Department>> GetAll()
    {
        return repository.GetAll();
    }
}