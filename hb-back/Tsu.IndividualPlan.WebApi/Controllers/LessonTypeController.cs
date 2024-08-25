using Microsoft.AspNetCore.Mvc;
using Tsu.IndividualPlan.WebApi.Dto;
using Tsu.IndividualPlan.WebApi.Extensions.Entities;
using Tsu.IndividualPlan.WebApi.Interfaces.Services;

namespace Tsu.IndividualPlan.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LessonTypeController : ControllerBase
{
    private readonly ILessonTypeService _service;

    public LessonTypeController(ILessonTypeService service)
    {
        _service = service;
    }

    [HttpGet("{Id}")]
    // TODO move to report service
    public async Task<ActionResult<LessonTypeDto>> GetByReportId(Guid Id)
    {
        try
        {
            var result = await _service.GetAllForEvent(Id);
            return Ok(result.Select(x => x.toDTO()).ToList());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}