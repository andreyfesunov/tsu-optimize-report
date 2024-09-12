using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tsu.IndividualPlan.Domain.Interfaces.Services;
using Tsu.IndividualPlan.Domain.Models.Project;
using Tsu.IndividualPlan.Transfer.Interfaces.Report;
using Tsu.IndividualPlan.WebApi.Dto.Report;
using Tsu.IndividualPlan.WebApi.Extensions.Entities;

namespace Tsu.IndividualPlan.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ReportController(
    IReportCreateService reportCreateService,
    IReportExportService reportExportService,
    IStateUserService service)
    : ControllerBase
{
    [HttpPost("{stateUserId:guid}/[action]")]
    public async Task<bool> CreateReport(Guid stateUserId, IFormFile file)
    {
        return await reportCreateService.CreateReport(stateUserId, file);
    }

    [HttpGet("{stateUserId:guid}/[action]")]
    public async Task<FileContentResult> ExportReport(Guid stateUserId)
    {
        var workbook = await reportExportService.ExportReport(stateUserId.ToString());

        using var stream = new MemoryStream();
        workbook.Write(stream);

        var result = new FileContentResult(stream.ToArray(), "application/vnd.ms-excel")
        {
            FileDownloadName = "individual-plan.xls"
        };

        return result;
    }

    [HttpPost("search")]
    public async Task<ActionResult<Pagination<ReportListDto>>> Search(Search search)
    {
        try
        {
            var result = await service.Search(search);
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
            var result = await service.GetById(id);
            return Ok(result.toDTO());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}