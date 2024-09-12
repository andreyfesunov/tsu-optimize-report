using Tsu.IndividualPlan.Domain.Models.Business;
using Tsu.IndividualPlan.Domain.Models.Project;

namespace Tsu.IndividualPlan.Domain.Interfaces.Repositories;

public interface IDepartmentRepository
{
    Task<Pagination<Department>> Search(Search search);
}