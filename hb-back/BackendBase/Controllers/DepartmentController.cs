using BackendBase.Dto;
using BackendBase.Interfaces;
using BackendBase.Models;
using MathNet.Numerics.Statistics.Mcmc;
using Microsoft.AspNetCore.Mvc;

namespace BackendBase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _service;

        public DepartmentController(IDepartmentService service)
        {
            _service = service;
        }


        [HttpPost("create")]
        public async Task<ActionResult<Department>> Create(Department entity)
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
        public async Task<ActionResult<DepartmentDto>> GetById(Guid Id)
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
        public async Task<ActionResult<ICollection<DepartmentDto>>> GetAll()
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
        public async Task<ActionResult<Department>> Update(Department entity)
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
        public async Task<ActionResult<PaginationDto<DepartmentDto>>> Search([FromBody] SearchDto searchDto)
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
