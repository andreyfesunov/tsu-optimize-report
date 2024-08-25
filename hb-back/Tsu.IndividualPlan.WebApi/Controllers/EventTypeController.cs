using Microsoft.AspNetCore.Mvc;
using Tsu.IndividualPlan.WebApi.Dto;
using Tsu.IndividualPlan.WebApi.Interfaces.Services;

namespace Tsu.IndividualPlan.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventTypeController : ControllerBase
{
    private readonly IEventTypeService _service;

    public EventTypeController(IEventTypeService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<EventTypeDto>>> GetAll()
    {
        try
        {
            var result = await _service.GetAll();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("searchMap")]
    public async Task<ActionResult<Dictionary<string, Pagination<EventTypeDto>>>> SearchMap(
        [FromBody] SearchDto searchDto
    )
    {
        try
        {
            var result = await _service.SearchMap(searchDto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{activityId:guid}/search")]
    public async Task<ActionResult<Pagination<EventTypeDto>>> Search(
        Guid activityId,
        [FromBody] SearchDto searchDto
    )
    {
        try
        {
            var result = await _service.Search(activityId, searchDto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("[action]")]
    public async Task<ActionResult<bool>> Assign([FromBody] EventTypeAssignDto dto)
    {
        try
        {
            await _service.Assign(dto);
            return Ok(true);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("getAll/{stateUserId:guid}/{workId:guid}")]
    public async Task<ActionResult<ICollection<EventTypeDto>>> GetAllForReport(
        Guid stateUserId,
        Guid workId
    )
    {
        try
        {
            var eventTypes = await _service.GetAllForReport(stateUserId, workId);
            return Ok(eventTypes);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}