using Tsu.IndividualPlan.WebApi.Interfaces.Repositories;
using Tsu.IndividualPlan.WebApi.Interfaces.Services;
using Tsu.IndividualPlan.WebApi.Models;

namespace Tsu.IndividualPlan.WebApi.Services;

public class LessonTypeService : ILessonTypeService
{
    protected readonly ILessonTypeRepository _repository;

    public LessonTypeService(ILessonTypeRepository repository)
    {
        _repository = repository;
    }

    public async Task<ICollection<LessonType>> GetAllForEvent(Guid stateUserId)
    {
        return await _repository.GetAllForReport(stateUserId);
    }
}