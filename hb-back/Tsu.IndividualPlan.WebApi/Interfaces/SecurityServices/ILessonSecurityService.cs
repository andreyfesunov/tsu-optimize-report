using Tsu.IndividualPlan.WebApi.Models;

namespace Tsu.IndividualPlan.WebApi.Interfaces.SecurityServices;

public interface ILessonSecurityService
{
    public Task validateCanUse(Lesson item);
    public Task validateCanCreate(Lesson item);
}