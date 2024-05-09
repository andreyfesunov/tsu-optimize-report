using BackendBase.Attributes;
using BackendBase.Models;
using BackendBase.Models.Enum;
using Microsoft.AspNetCore.Mvc;

namespace BackendBase.Helpers.CRUD
{
    public class CRUDControllerBase<TEntity> : ControllerBase where TEntity : Base
    {
        protected ICRUDServiceBase<TEntity> _service;


        public CRUDControllerBase(ICRUDServiceBase<TEntity> serivce)
        {
            _service = serivce;
        }


        [HttpPost("create")]
        public async Task<ActionResult<TEntity>> Create(TEntity entity)
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
        public async Task<ActionResult<TEntity>> GetById(Guid Id)
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
        public async Task<ActionResult<ICollection<TEntity>>> GetAll()
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
        public async Task<ActionResult<TEntity>> Update(TEntity entity)
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
    }
}
