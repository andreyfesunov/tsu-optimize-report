using Microsoft.AspNetCore.Mvc;
using Tsu.IndividualPlan.WebApi.Dto;
using Tsu.IndividualPlan.WebApi.Dto.CreateDto;
using Tsu.IndividualPlan.WebApi.Extensions.Entities;
using Tsu.IndividualPlan.WebApi.Interfaces.Services;

namespace Tsu.IndividualPlan.WebApi.Controllers;

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
                    result.PageNumber,
                    result.PageSize,
                    result.TotalPages,
                    result.Entities.Select(u => u.toDTO()).ToList()
                )
            );
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}