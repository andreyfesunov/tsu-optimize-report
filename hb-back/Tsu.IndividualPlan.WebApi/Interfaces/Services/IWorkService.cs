using Tsu.IndividualPlan.WebApi.Models;

namespace Tsu.IndividualPlan.WebApi.Interfaces.Services;

public interface IWorkService
{
    Task<ICollection<Work>> GetAll();
}