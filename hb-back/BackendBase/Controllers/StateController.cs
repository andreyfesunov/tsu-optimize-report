using AutoMapper;
using BackendBase.Dto;
using BackendBase.Dto.CreateDto;
using BackendBase.Helpers;
using BackendBase.Interfaces.Repositories;
using BackendBase.Interfaces.Services;
using BackendBase.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackendBase.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StateController : ControllerBase
{
    private readonly IStateService _service;
    private readonly IMapper _mapper;

    public StateController(
            IStateService service,
            IMapper mapper
            )
    {
        _service = service;
        _mapper = mapper;
    }


    [HttpPut("")]
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
    public async Task<ActionResult<bool>> Assign(StateUserCreateDto dto) {
        try {
            var result = await _service.Assign(dto);
            return Ok(result);
        }
        catch (Exception ex) {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("search")]
    public async Task<ActionResult<Pagination<StateDto>>> Search([FromBody] SearchDto searchDto)
    {
        try
        {
            var result = await _service.Search(searchDto);
            return Ok(new Pagination<UserDto>
            {
                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
                TotalPages = result.TotalPages,
                Entities = result.Entities.Select(u => _mapper.Map<UserDto>(u)).ToList()
            });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
