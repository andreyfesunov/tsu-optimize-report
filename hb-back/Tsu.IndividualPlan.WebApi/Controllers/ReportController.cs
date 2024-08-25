using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tsu.IndividualPlan.WebApi.Dto;
using Tsu.IndividualPlan.WebApi.Dto.Report;
using Tsu.IndividualPlan.WebApi.Extensions.Entities;
using Tsu.IndividualPlan.WebApi.Interfaces.Services;
using Tsu.IndividualPlan.WebApi.Interfaces.Services.Report;

namespace Tsu.IndividualPlan.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ReportController : ControllerBase
{
    private readonly IReportCreateService _reportCreateService;
    private readonly IReportExportService _reportExportService;
    private readonly IStateUserService _service;

    public ReportController(
        IReportCreateService reportCreateService,
        IReportExportService reportExportService,
        IStateUserService service
    )
    {
        _reportCreateService = reportCreateService;
        _reportExportService = reportExportService;
        _service = service;
    }

    [HttpPost("{stateUserId:guid}/[action]")]
    public async Task<bool> CreateReport(Guid stateUserId, IFormFile file)
    {
        return await _reportCreateService.CreateReport(stateUserId, file);
    }

    [HttpGet("{stateUserId:guid}/[action]")]
    public async Task<FileContentResult> ExportReport(Guid stateUserId)
    {
        var workbook = await _reportExportService.ExportReport(stateUserId.ToString());

        using var stream = new MemoryStream();
        workbook.Write(stream);

        var result = new FileContentResult(stream.ToArray(), "application/vnd.ms-excel")
        {
            FileDownloadName = "individual-plan.xls"
        };

        return result;
    }

    [HttpPost("search")]
    public async Task<ActionResult<Pagination<ReportListDto>>> Search(SearchDto searchDto)
    {
        try
        {
            var result = await _service.Search(searchDto);
            return Ok(
                new Pagination<ReportListDto>(
                    result.PageNumber,
                    result.PageSize,
                    result.TotalPages,
                    result.Entities.Select(x => x.toListDTO()).ToList()
                )
            );
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ReportDetailDto>> Detail(Guid id)
    {
        try
        {
            var result = await _service.GetById(id);
            return Ok(result.toDTO());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}