using Tsu.IndividualPlan.WebApi.Dto.Lesson;
using Tsu.IndividualPlan.WebApi.Models;

namespace Tsu.IndividualPlan.WebApi.Interfaces.Services;

public interface ILessonService
{
    Task<Lesson> AddEntity(LessonCreateDto dto);
    Task<Lesson> Update(LessonUpdateDto dto);
    Task<bool> DeleteById(Guid entityId);
}