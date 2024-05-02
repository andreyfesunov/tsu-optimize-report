using BackendBase.Interfaces;
using BackendBase.Models;
using BackendBase.Repositories;
using BackendBase.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackendBase.Dto;
using BackendBase.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendBase.Helpers
{
    public class CRUDControllerBase<TEntity> : ControllerBase where TEntity : Base
    {
        protected ICRUDServiceBase<TEntity> _service;


        public CRUDControllerBase(ICRUDServiceBase<TEntity> serivce)
        {
            _service = serivce;
        }


        [HttpGet("create")]
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

        [HttpGet("get")]
        public async Task<ActionResult<TEntity>> GetById(int id)
        {
            try
            {
                var result = await _service.GetById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("update")]
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

        [HttpGet("delete")]
        public async Task<ActionResult<bool>> Delete(TEntity entity)
        {
            try
            {
                var result = await _service.Delete(entity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
