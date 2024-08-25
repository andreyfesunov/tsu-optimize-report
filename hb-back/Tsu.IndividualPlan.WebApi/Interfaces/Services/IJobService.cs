using Tsu.IndividualPlan.WebApi.Models;

namespace Tsu.IndividualPlan.WebApi.Interfaces.Services;

public interface IJobService
{
    Task<ICollection<Job>> GetAll();
}