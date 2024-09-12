using Tsu.IndividualPlan.Domain.Models.Business;

namespace Tsu.IndividualPlan.Domain.Interfaces.SecurityServices;

public interface ILessonSecurityService
{
    public Task validateCanUse(Lesson item);
    public Task validateCanCreate(Lesson item);
}