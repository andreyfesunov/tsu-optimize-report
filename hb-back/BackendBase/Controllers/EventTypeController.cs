using BackendBase.Dto;
using BackendBase.Interfaces.Services;
using BackendBase.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackendBase.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventTypeController : ControllerBase
{
    private readonly IEventTypeService _service;

    public EventTypeController(IEventTypeService service)
    {
        _service = service;
    }

    [HttpPost("create")]
    public async Task<ActionResult<EventType>> Create(EventType entity)
    {
        try
        {
            var result = await _service.AddEntity(entity);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{Id}")]
    public async Task<ActionResult<EventTypeDto>> GetById(Guid Id)
    {
        try
        {
            var result = await _service.GetById(Id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("getAll")]
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

    [HttpPut("update")]
    public async Task<ActionResult<EventType>> Update(EventType entity)
    {
        try
        {
            var result = await _service.Update(entity);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{entityId}")]
    public async Task<ActionResult<bool>> DeleteById(Guid entityId)
    {
        try
        {
            var result = await _service.DeleteById(entityId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("search")]
    public async Task<ActionResult<Pagination<EventTypeDto>>> Search([FromBody] SearchDto searchDto)
    {
        try
        {
            var result = await _service.Search(searchDto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpPost("searchMap")]
    public async Task<ActionResult<Dictionary<string, Pagination<EventTypeDto>>>> SearchMap(
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
    public async Task<ActionResult<Pagination<EventTypeDto>>> Search(Guid activityId, [FromBody] SearchDto searchDto)
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