using AutoMapper;
using BackendBase.Dto;
using BackendBase.Helpers;
using BackendBase.Interfaces.Repositories;
using BackendBase.Interfaces.Services;
using BackendBase.Models;
using BackendBase.Models.Enum;
using Microsoft.AspNetCore.Mvc;

namespace BackendBase.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly MappingHelper<User, UserDto> _mapper;
    private readonly IUserRepository _repository;
    private readonly IUserService _service;

    public UserController(IUserService service, IUserRepository repository, IMapper mapper)
    {
        _service = service;
        _repository = repository;
        _mapper = new MappingHelper<User, UserDto>(mapper);
    }

    [HttpGet("getAll")]
    public async Task<ActionResult<List<UserDto>>> GetAll()
    {
        try
        {
            var users = await _repository.GetAll();
            return Ok(_mapper.ToDto(users));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{userId:guid}")]
    public async Task<ActionResult<UserDto>> GetById(Guid userId)
    {
        try
        {
            var user = await _repository.GetById(userId);
            return Ok(_mapper.ToDto(user));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("log-in")]
    public async Task<ActionResult<UserLoginDto>> Login([FromBody] LoginDto loginDto)
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
            var result = await _repository.Search(searchDto);
            return Ok(_mapper.ToDto(result));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}