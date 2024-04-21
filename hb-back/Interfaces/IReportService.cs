using BackendBase.Dto;

namespace BackendBase.Interfaces
{
    public interface IReportService
    {
        Task<int> CreateReport(IFormFile file);
    }
}
