using BackendBase.Interfaces.Repositories;
using BackendBase.Interfaces.Services;
using BackendBase.Models;

namespace BackendBase.Services;

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
