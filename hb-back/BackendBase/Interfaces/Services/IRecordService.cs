using BackendBase.Dto;

namespace BackendBase.Interfaces.Services;

public interface IRecordService
{
    Task<RecordDto[]> Get(Guid stateUserId);
}