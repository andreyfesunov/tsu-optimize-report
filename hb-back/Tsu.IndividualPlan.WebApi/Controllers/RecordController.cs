using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tsu.IndividualPlan.Domain.Interfaces.Services;
using Tsu.IndividualPlan.Domain.Models.Business;
using Tsu.IndividualPlan.WebApi.Dto;
using Tsu.IndividualPlan.WebApi.Extensions.Entities;

namespace Tsu.IndividualPlan.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class RecordController(IRecordService recordService) : ControllerBase
{
    [HttpGet("{stateUserId:guid}/{semestrId}")]
    public async Task<ActionResult<RecordDto[]>> Get(Guid stateUserId, [FromRoute] int semestrId)
    {
        try
        {
            var result = await recordService.GetForReport(stateUserId, semestrId);
            return Ok(result.Select<Record, RecordDto>(x => x.toDTO()).ToList());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}