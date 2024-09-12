using Tsu.IndividualPlan.Domain.Models.Business;

namespace Tsu.IndividualPlan.Domain.Interfaces.Repositories;

public interface IActivityRepository
{
    Task<ICollection<Activity>> GetAll();
}