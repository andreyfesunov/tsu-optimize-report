using BackendBase.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BackendBase.Models;
using BackendBase.Dto.Report;
using BackendBase.Dto;

namespace BackendBase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateUserController : ControllerBase
    {
        private readonly IStateUserService _service;

        public StateUserController(IStateUserService service)
        {
            _service = service;
        }


        [HttpPost("create")]
        public async Task<ActionResult<StateUser>> Create(StateUser entity)
        {
            try
            {
                var result = await _service.AddEntity(entity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<StateUserDto>> GetById(Guid Id)
        {
            try
            {
                var result = await _service.GetById(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getAll")]
        public async Task<ActionResult<ICollection<StateUserDto>>> GetAll()
        {
            try
            {
                var result = await _service.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update")]
        public async Task<ActionResult<StateUser>> Update(StateUser entity)
        {
            try
            {
                var result = await _service.Update(entity);
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

        [HttpPost("search")]
        public async Task<ActionResult<PaginationDto<StateUserDto>>> Search([FromBody] SearchDto searchDto)
        {
            try
            {
                var result = await _service.Search(searchDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
