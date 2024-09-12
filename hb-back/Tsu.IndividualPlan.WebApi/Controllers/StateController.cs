using Microsoft.AspNetCore.Mvc;
using Tsu.IndividualPlan.Domain.Dto.IndividualPlan;
using Tsu.IndividualPlan.Domain.Dto.State;
using Tsu.IndividualPlan.Domain.Interfaces.Services;
using Tsu.IndividualPlan.Domain.Models.Project;
using Tsu.IndividualPlan.WebApi.Dto;
using Tsu.IndividualPlan.WebApi.Extensions.Entities;

namespace Tsu.IndividualPlan.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StateController(IStateService service) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<string>> Create(StateCreateDto entity)
    {
        try
        {
            var result = await service.Create(entity);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("assign")]
    public async Task<ActionResult<bool>> Assign(IndividualPlanCreateDto dto)
    {
        try
        {
            var result = await service.Assign(dto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("search")]
    public async Task<ActionResult<Pagination<StateDto>>> Search([FromBody] Search search)
    {
        try
        {
            var result = await service.Search(search);
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