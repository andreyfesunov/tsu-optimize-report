using Tsu.IndividualPlan.WebApi.Models;

namespace Tsu.IndividualPlan.WebApi.Interfaces.Repositories;

public interface IJobRepository
{
    Task<ICollection<Job>> GetAll();
}