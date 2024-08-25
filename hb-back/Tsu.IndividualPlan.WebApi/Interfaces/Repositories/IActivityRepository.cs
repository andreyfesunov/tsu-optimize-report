using Tsu.IndividualPlan.WebApi.Models;

namespace Tsu.IndividualPlan.WebApi.Interfaces.Repositories;

public interface IActivityRepository
{
    Task<ICollection<Activity>> GetAll();
}