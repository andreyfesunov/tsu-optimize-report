using BackendBase.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BackendBase.Models;
using BackendBase.Dto;

namespace BackendBase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstituteController : ControllerBase
    {
        private readonly IInstituteService _service;

        public InstituteController(IInstituteService service)
        {
            _service = service;
        }

        [HttpPost("create")]
        public async Task<ActionResult<Institute>> Create(Institute entity)
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
        public async Task<ActionResult<InstituteDto>> GetById(Guid Id)
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
        public async Task<ActionResult<ICollection<InstituteDto>>> GetAll()
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
        public async Task<ActionResult<Institute>> Update(Institute entity)
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
        public async Task<ActionResult<PaginationDto<InstituteDto>>> Search([FromBody] SearchDto searchDto)
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
