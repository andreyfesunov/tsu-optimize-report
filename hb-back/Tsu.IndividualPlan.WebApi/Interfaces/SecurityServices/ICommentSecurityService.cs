using Tsu.IndividualPlan.WebApi.Models;

namespace Tsu.IndividualPlan.WebApi.Interfaces.SecurityServices;

public interface ICommentSecurityService
{
    public Task validateCanUse(Comment item);
    public Task validateCanCreate(Comment item);
}