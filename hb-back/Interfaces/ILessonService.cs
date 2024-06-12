using BackendBase.Dto;
using BackendBase.Dto.Lesson;
using BackendBase.Helpers.CRUD;
using BackendBase.Models;

namespace BackendBase.Interfaces;

public interface ILessonService : ICRUDServiceBase<Lesson, LessonDto>
{
    Task<LessonDto> Update(LessonUpdateDto entity);
}