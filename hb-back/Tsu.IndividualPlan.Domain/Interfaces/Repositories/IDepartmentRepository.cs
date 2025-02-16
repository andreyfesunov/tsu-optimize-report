using Tsu.IndividualPlan.Domain.Models.Business;
using Tsu.IndividualPlan.Domain.Models.Project;

namespace Tsu.IndividualPlan.Domain.Interfaces.Repositories;

public interface IDepartmentRepository
{
    Task<ICollection<Department>> GetAll();
    Task<Department> GetById(Guid id);
    Task<Pagination<Department>> Search(Search search);
}