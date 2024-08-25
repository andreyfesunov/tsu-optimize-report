using BackendBase.Models;

namespace BackendBase.Interfaces.Services;

public interface IRecordService
{
    Task<ICollection<Record>> GetForReport(Guid stateUserId);
}
