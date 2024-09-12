using Tsu.IndividualPlan.Domain.Models.Business;

namespace Tsu.IndividualPlan.Domain.Interfaces.Services;

public interface ILessonTypeService
{
    Task<ICollection<LessonType>> GetAllForEvent(Guid stateUserId);
}