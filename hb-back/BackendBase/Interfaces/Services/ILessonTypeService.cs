using BackendBase.Models;

namespace BackendBase.Interfaces.Services;

public interface ILessonTypeService
{
    Task<ICollection<LessonType>> GetAllForEvent(Guid stateUserId);
}
