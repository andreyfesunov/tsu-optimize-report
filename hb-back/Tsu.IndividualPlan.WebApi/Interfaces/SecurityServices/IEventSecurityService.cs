using Tsu.IndividualPlan.WebApi.Models;

namespace Tsu.IndividualPlan.WebApi.Interfaces.SecurityServices;

public interface IEventSecurityService
{
    public Task validateCanUse(Event item);
    public Task validateCanCreate(Event item);
}