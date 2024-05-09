using BackendBase.Dto;
using BackendBase.Interfaces;
using BackendBase.Models.Enum;
using Microsoft.AspNetCore.Mvc;

namespace BackendBase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("get-all")]
        public async Task<ActionResult<List<UserDto>>> GetAll()
        {
            try
            {
                var users = await _userService.GetAllUsers();
                return Ok(users);
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
                var user = await _userService.GetUserById(userId);
                return Ok(user);
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
                var userLoginDto = await _userService.LogIn(loginDto);
                return Ok(userLoginDto);
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
                var userId = await _userService.Reg(registrationDto);
                return Ok(userId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
