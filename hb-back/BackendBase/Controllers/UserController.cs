using BackendBase.Dto;
using BackendBase.Extensions.Entities;
using BackendBase.Interfaces.Services;
using BackendBase.Models;
using BackendBase.Models.Enum;
using Microsoft.AspNetCore.Mvc;

namespace BackendBase.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<UserDto>>> GetAll()
    {
        try
        {
            var users = await _service.GetAll();
            return Ok(users.Select(u => u.toDTO()));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserDto>> GetById(Guid id)
    {
        try
        {
            var user = await _service.GetById(id);
            return Ok(user.toDTO());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("log-in")]
    public async Task<ActionResult<UserLoginDto>> LogIn([FromBody] LoginDto loginDto)
    {
        try
        {
            var token = await _service.LogIn(loginDto);
            return Ok(new UserLoginDto { Token = token });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("reg")]
    public async Task<ActionResult<RoleUserEnum>> Reg([FromBody] RegistrationDto registrationDto)
    {
        try
        {
            var role = await _service.Reg(registrationDto);
            return Ok(role);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("search")]
    public async Task<ActionResult<Pagination<UserDto>>> Search(SearchDto searchDto)
    {
        try
        {
            var result = await _service.Search(searchDto);

            return Ok(new Pagination<UserDto>
            {
                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
                TotalPages = result.TotalPages,
                Entities = result.Entities.Select(u => u.toDTO()).ToList()
            });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
