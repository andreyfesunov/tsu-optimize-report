using System.Reflection.Metadata;
using BackendBase.Dto;
using BackendBase.Interfaces;
using BackendBase.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendBase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpPost("[action]")]
        public async Task<int> CreateReport(IFormFile file)
        {
            return await _reportService.CreateReport(file);
        }
    }
}
