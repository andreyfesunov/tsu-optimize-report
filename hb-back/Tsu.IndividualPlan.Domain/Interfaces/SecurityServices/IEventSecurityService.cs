using Tsu.IndividualPlan.Domain.Models.Business;

namespace Tsu.IndividualPlan.Domain.Interfaces.SecurityServices;

public interface IEventSecurityService
{
    public Task validateCanUse(Event item);
    public Task validateCanCreate(Event item);
}