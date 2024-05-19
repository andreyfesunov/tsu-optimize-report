using BackendBase.Dto;
using BackendBase.Dto.CreateDto;
using BackendBase.Helpers.CRUD;
using BackendBase.Interfaces;
using BackendBase.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackendBase.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StateController : CRUDControllerBase<State, StateDto>
{
    private readonly IStateService _stateService;

    public StateController(IStateService service)
        : base(service)
    {
        _stateService = service;
    }

    [HttpPost("createWithDto")]
    public async Task<ActionResult<State>> Create(StateCreateDto entity)
    {
        try
        {
            var result = await _stateService.AddStateWithCreateDto(entity);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("assign")]
    public async Task<ActionResult<bool>> Assign(StateUserCreateDto entity)
    {
        try
        {
            var result = await _stateService.Assign(entity);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}