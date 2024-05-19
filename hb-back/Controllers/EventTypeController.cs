using BackendBase.Dto;
using BackendBase.Helpers.CRUD;
using BackendBase.Interfaces;
using BackendBase.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackendBase.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventTypeController : CRUDControllerBase<EventType, EventTypeDto>
{
    private readonly IEventTypeService _service;

    public EventTypeController(IEventTypeService service)
        : base(service)
    {
        _service = service;
    }

    [HttpPost("searchMap")]
    public async Task<ActionResult<Dictionary<string, PaginationDto<EventTypeDto>>>> SearchMap(
        [FromBody] SearchDto searchDto)
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
    public async Task<ActionResult<PaginationDto<EventTypeDto>>> Search(Guid activityId, [FromBody] SearchDto searchDto)
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

    [HttpPost("assign")]
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
}