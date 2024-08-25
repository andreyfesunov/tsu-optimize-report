using Tsu.IndividualPlan.WebApi.Models;

namespace Tsu.IndividualPlan.WebApi.Interfaces.Services;

public interface ILessonTypeService
{
    Task<ICollection<LessonType>> GetAllForEvent(Guid stateUserId);
}