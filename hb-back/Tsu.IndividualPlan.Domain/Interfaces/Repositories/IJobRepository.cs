using Tsu.IndividualPlan.Domain.Models.Business;

namespace Tsu.IndividualPlan.Domain.Interfaces.Repositories;

public interface IJobRepository
{
    Task<ICollection<Job>> GetAll();
    Task<Job> AddEntity(Job entity);
}