using Tsu.IndividualPlan.WebApi.Models;

namespace Tsu.IndividualPlan.WebApi.Interfaces.Services;

public interface IActivityService
{
    Task<ICollection<Activity>> GetAll();
}