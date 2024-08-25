using Tsu.IndividualPlan.WebApi.Models;

namespace Tsu.IndividualPlan.WebApi.Interfaces.Repositories;

public interface ILessonTypeRepository
{
    Task<LessonType> AddEntity(LessonType entity);
    Task<LessonType?> GetLessonTypeByName(string name);
    Task<ICollection<LessonType>> GetAllForReport(Guid stateUserId);
}