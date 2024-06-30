using BackendBase.Dto;
using BackendBase.Dto.Lesson;
using BackendBase.Models;

namespace BackendBase.Interfaces;

public interface ILessonService
{
    Task<Lesson> AddEntity(Lesson entity);
    Task<LessonDto> GetById(Guid id);
    Task<ICollection<LessonDto>> GetAll();
    Task<Lesson> Update(Lesson entity);
    Task<bool> DeleteById(Guid entityId);
    Task<PaginationDto<LessonDto>> Search(SearchDto searchDto);
    Task<LessonDto> Update(LessonUpdateDto entity);
}