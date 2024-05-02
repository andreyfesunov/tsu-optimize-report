using BackendBase.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendBase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpPost("{stateUserId:guid}/[action]")]
        public async Task<int> CreateReport(Guid stateUserId, IFormFile file)
        {
            return await _reportService.CreateReport(stateUserId, file);
        }
    }
}
