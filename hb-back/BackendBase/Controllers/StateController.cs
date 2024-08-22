using BackendBase.Dto;
using BackendBase.Dto.CreateDto;
using BackendBase.Extensions.Entities;
using BackendBase.Interfaces.Services;
using BackendBase.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackendBase.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StateController : ControllerBase
{
    private readonly IStateService _service;

    public StateController(IStateService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<string>> Create(StateCreateDto entity)
    {
        try
        {
            var result = await _service.Create(entity);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("assign")]
    public async Task<ActionResult<bool>> Assign(StateUserCreateDto dto)
    {
        try
        {
            var result = await _service.Assign(dto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("search")]
    public async Task<ActionResult<Pagination<StateDto>>> Search([FromBody] SearchDto searchDto)
    {
        try
        {
            var result = await _service.Search(searchDto);
            return Ok(
                new Pagination<StateDto>(
                    PageNumber: result.PageNumber,
                    PageSize: result.PageSize,
                    TotalPages: result.TotalPages,
                    Entities: result.Entities.Select(u => u.toDTO()).ToList()
                )
            );
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
