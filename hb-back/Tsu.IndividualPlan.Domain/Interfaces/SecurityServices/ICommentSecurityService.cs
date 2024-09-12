using Tsu.IndividualPlan.Domain.Models.Business;

namespace Tsu.IndividualPlan.Domain.Interfaces.SecurityServices;

public interface ICommentSecurityService
{
    public Task validateCanUse(Comment item);
    public Task validateCanCreate(Comment item);
}