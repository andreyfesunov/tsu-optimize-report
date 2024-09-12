using Microsoft.AspNetCore.Mvc;
using Tsu.IndividualPlan.Domain.Dto.EventType;
using Tsu.IndividualPlan.Domain.Interfaces.Services;
using Tsu.IndividualPlan.Domain.Models.Project;
using Tsu.IndividualPlan.WebApi.Dto;
using Tsu.IndividualPlan.WebApi.Extensions.Entities;

namespace Tsu.IndividualPlan.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventTypeController(IEventTypeService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ICollection<EventTypeDto>>> GetAll()
    {
        try
        {
            var result = await service.GetAll();
            return Ok(result.Select(x => x.toDTO()).ToList());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("searchMap")]
    public async Task<ActionResult<Dictionary<string, Pagination<EventTypeDto>>>> SearchMap(
        [FromBody] Search search
    )
    {
        try
        {
            var searchMap = await service.SearchMap(search);
            var result = searchMap.ToDictionary(
                kvp => kvp.Key,
                kvp => new Pagination<EventTypeDto>(
                    kvp.Value.PageNumber,
                    kvp.Value.PageSize,
                    kvp.Value.TotalPages,
                    kvp.Value.Entities.Select(x => x.toDTO()).ToList()
                )
            );
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
        [FromBody] Search search
    )
    {
        try
        {
            var result = await service.Search(activityId, search);
            return Ok(
                new Pagination<EventTypeDto>(
                    result.PageNumber,
                    result.PageSize,
                    result.TotalPages,
                    result.Entities.Select(x => x.toDTO()).ToList()
                )
            );
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
            await service.Assign(dto);
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
            var eventTypes = await service.GetAllForReport(stateUserId, workId);
            return Ok(eventTypes.Select(x => x.toDTO()).ToList());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}