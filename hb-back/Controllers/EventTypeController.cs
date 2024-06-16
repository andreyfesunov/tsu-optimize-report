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

    [HttpGet("getAll/{stateUserId:guid}/{workId:guid}/{first:bool}")]
    public async Task<ActionResult<ICollection<EventTypeDto>>> GetAllForReport(Guid stateUserId, Guid workId,
        bool first)
    {
        try
        {
            var eventTypes = await _service.GetAllForReport(stateUserId, workId, first);
            return Ok(eventTypes);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{activityId:guid}/{entityTypeId:guid}")]
    public async Task<ActionResult<bool>> Delete(Guid activityId, Guid entityTypeId)
    {
        try
        {
            var result = await _service.Delete(activityId, entityTypeId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}