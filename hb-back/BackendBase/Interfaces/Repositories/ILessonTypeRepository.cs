using BackendBase.Models;

namespace BackendBase.Interfaces.Repositories;

public interface ILessonTypeRepository
{
    Task<LessonType> AddEntity(LessonType entity);
    Task<LessonType?> GetLessonTypeByName(string name);
    Task<ICollection<LessonType>> GetAllForReport(Guid stateUserId);
}
