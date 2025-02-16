using Tsu.IndividualPlan.Domain.Models.Business;

namespace Tsu.IndividualPlan.Domain.Interfaces.Services;

public interface IDepartmentService
{
    Task<ICollection<Department>> GetAll();
}