using Microsoft.AspNetCore.Mvc;
using Tsu.IndividualPlan.Domain.Interfaces.Services;
using Tsu.IndividualPlan.Domain.Models.Business;
using Tsu.IndividualPlan.WebApi.Dto;
using Tsu.IndividualPlan.WebApi.Extensions.Entities;

namespace Tsu.IndividualPlan.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LessonTypeController(ILessonTypeService service) : ControllerBase
{
    [HttpGet("{Id:guid}")]
// TODO move to report service
    public async Task<ActionResult<LessonTypeDto>> GetByReportId(Guid Id)
    {
        try
        {
            var result = await service.GetAllForEvent(Id);
            return Ok(result.Select<LessonType, LessonTypeDto>(x => x.toDTO()).ToList());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}