using Tsu.IndividualPlan.Domain.Models.Business;

namespace Tsu.IndividualPlan.Domain.Interfaces.Services;

public interface IWorkService
{
    Task<ICollection<Work>> GetAll();
}