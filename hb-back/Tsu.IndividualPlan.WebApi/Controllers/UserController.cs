using Microsoft.AspNetCore.Mvc;
using Tsu.IndividualPlan.Domain.Dto.Auth;
using Tsu.IndividualPlan.Domain.Enumerations;
using Tsu.IndividualPlan.Domain.Interfaces.Services;
using Tsu.IndividualPlan.Domain.Models.Project;
using Tsu.IndividualPlan.WebApi.Dto;
using Tsu.IndividualPlan.WebApi.Extensions.Entities;

namespace Tsu.IndividualPlan.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IUserService service,
    IStateUserService stateUserService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ICollection<UserDto>>> GetAll()
    {
        try
        {
            var users = await service.GetAll();
            return Ok(users.Select(u => u.toDTO()).ToList());
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
            var user = await service.GetById(id);
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
            var token = await service.LogIn(loginDto);
            return Ok(new UserLoginDto(token));
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
            var role = await service.Reg(registrationDto);
            return Ok(role);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("search")]
    public async Task<ActionResult<Pagination<UserDto>>> Search(Search search)
    {
        try
        {
            var result = await service.Search(search);

            return Ok(
                new Pagination<UserDto>(
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

    [HttpPost("GetUserAllStates")]
    public async Task<ActionResult<Pagination<UserAllStatesDto>>> GetUserAllStates(Search search)
    {
        try
        {
            var result = await stateUserService.GetUserAllStates(search);

            return Ok(
                new Pagination<UserAllStatesDto>(
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