using Tsu.IndividualPlan.Domain.Dto.Lesson;
using Tsu.IndividualPlan.Domain.Models.Business;

namespace Tsu.IndividualPlan.Domain.Interfaces.Services;

public interface ILessonService
{
    Task<Lesson> AddEntity(LessonCreateDto dto);
    Task<Lesson> Update(LessonUpdateDto dto);
    Task<bool> DeleteById(Guid entityId);
}