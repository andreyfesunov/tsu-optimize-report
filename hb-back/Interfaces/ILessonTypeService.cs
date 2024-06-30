using BackendBase.Dto;
using BackendBase.Models;

namespace BackendBase.Interfaces;

public interface ILessonTypeService
{
    Task<LessonType> AddEntity(LessonType entity);
    Task<LessonTypeDto> GetById(Guid id);
    Task<ICollection<LessonTypeDto>> GetAll();
    Task<LessonType> Update(LessonType entity);
    Task<bool> DeleteById(Guid entityId);
    Task<PaginationDto<LessonTypeDto>> Search(SearchDto searchDto);
    Task<ICollection<LessonTypeDto>> GetAllForEvent(Guid stateUserId);
}