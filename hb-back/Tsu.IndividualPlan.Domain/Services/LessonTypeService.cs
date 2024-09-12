using Tsu.IndividualPlan.Domain.Interfaces.Repositories;
using Tsu.IndividualPlan.Domain.Interfaces.Services;
using Tsu.IndividualPlan.Domain.Models.Business;

namespace Tsu.IndividualPlan.Domain.Services;

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