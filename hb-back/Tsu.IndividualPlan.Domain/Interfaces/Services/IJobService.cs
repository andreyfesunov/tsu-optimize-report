using Tsu.IndividualPlan.Domain.Models.Business;

namespace Tsu.IndividualPlan.Domain.Interfaces.Services;

public interface IJobService
{
    Task<ICollection<Job>> GetAll();
}