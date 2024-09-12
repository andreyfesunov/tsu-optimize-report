using Tsu.IndividualPlan.Domain.Models.Business;

namespace Tsu.IndividualPlan.Domain.Interfaces.Services;

public interface IActivityService
{
    Task<ICollection<Activity>> GetAll();
}