using AutoMapper;
using BackendBase.Dto;
using BackendBase.Dto.Event;
using BackendBase.Interfaces;
using BackendBase.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackendBase.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IEventService _service;

    public EventController(IEventService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpPost("[action]")]
    public async Task<ActionResult<EventDto>> Create([FromBody] EventCreateDto dto)
    {
        try
        {
            var result = await _service.AddEntity(_mapper.Map<Event>(dto));
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("[action]")]
    public async Task<ActionResult<EventDto>> Update([FromBody] EventUpdateDto dto)
    {
        try
        {
            var result = await _service.Update(dto);
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
}