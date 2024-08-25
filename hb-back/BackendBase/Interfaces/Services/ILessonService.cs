using BackendBase.Dto.Lesson;
using BackendBase.Models;

namespace BackendBase.Interfaces.Services;

public interface ILessonService
{
    Task<Lesson> AddEntity(LessonCreateDto dto);
    Task<Lesson> Update(LessonUpdateDto dto);
    Task<bool> DeleteById(Guid entityId);
}
