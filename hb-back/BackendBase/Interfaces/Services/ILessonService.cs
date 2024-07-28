using BackendBase.Dto.Lesson;
using BackendBase.Models;

namespace BackendBase.Interfaces.Services;

public interface ILessonService
{
    Task<Lesson> AddEntity(Lesson entity);
    Task<Lesson> Update(LessonUpdateDto entity);
    Task<bool> DeleteById(Guid entityId);
}
