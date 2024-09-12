using Microsoft.AspNetCore.Mvc;
using Tsu.IndividualPlan.Domain.Dto.Event;
using Tsu.IndividualPlan.Domain.Interfaces.Services;
using Tsu.IndividualPlan.WebApi.Dto;
using Tsu.IndividualPlan.WebApi.Extensions.Entities;

namespace Tsu.IndividualPlan.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventController(IEventService service) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<EventDto>> Create([FromBody] EventCreateDto dto)
    {
        try
        {
            var entity = await service.AddEntity(dto);
            return Ok(entity.toDTO());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    public async Task<ActionResult<EventDto>> Update([FromBody] EventUpdateDto dto)
    {
        try
        {
            var entity = await service.Update(dto);
            return Ok(entity.toDTO());
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
            var result = await service.DeleteById(entityId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}