using Tsu.IndividualPlan.Domain.Models.Business;

namespace Tsu.IndividualPlan.Domain.Interfaces.Repositories;

public interface ILessonTypeRepository
{
    Task<LessonType> AddEntity(LessonType entity);
    Task<LessonType?> GetLessonTypeByName(string name);
    Task<ICollection<LessonType>> GetAllForReport(Guid stateUserId);
}