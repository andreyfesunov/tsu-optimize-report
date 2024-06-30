using BackendBase.Dto;

namespace BackendBase.Interfaces;

public interface IRecordService
{
    Task<RecordDto[]> Get(Guid stateUserId);
}