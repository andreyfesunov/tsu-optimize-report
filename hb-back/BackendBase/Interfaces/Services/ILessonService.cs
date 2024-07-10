using BackendBase.Dto;
using BackendBase.Dto.Lesson;
using BackendBase.Models;

namespace BackendBase.Interfaces.Services;

public interface ILessonService
{
    Task<Lesson> AddEntity(Lesson entity);
    Task<LessonDto> GetById(Guid id);
    Task<ICollection<LessonDto>> GetAll();
    Task<Lesson> Update(Lesson entity);
    Task<bool> DeleteById(Guid entityId);
    Task<Pagination<LessonDto>> Search(SearchDto searchDto);
    Task<LessonDto> Update(LessonUpdateDto entity);
}