using Tsu.IndividualPlan.Domain.Models.Business;

namespace Tsu.IndividualPlan.Domain.Interfaces.Repositories;

public interface ILessonRepository
{
    Task<Lesson> AddEntity(Lesson entity);
    Task<Lesson> GetById(Guid id);
    Task<Lesson> UpdateEntity(Lesson entity);
    Task<bool> Delete(Lesson entity);
}