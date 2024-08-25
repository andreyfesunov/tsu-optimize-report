using Tsu.IndividualPlan.WebApi.Interfaces.Repositories;
using Tsu.IndividualPlan.WebApi.Interfaces.Services;
using Tsu.IndividualPlan.WebApi.Models;

namespace Tsu.IndividualPlan.WebApi.Services;

public class RecordService : IRecordService
{
    private readonly IRecordRepository _recordRepository;

    public RecordService(IRecordRepository recordRepository)
    {
        _recordRepository = recordRepository;
    }

    public async Task<ICollection<Record>> GetForReport(Guid stateUserId)
    {
        return await _recordRepository.Get(stateUserId);
    }
}