using BackendBase.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BackendBase.Models;
using BackendBase.Helpers.CRUD;
using BackendBase.Dto;
using AutoMapper;

namespace BackendBase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonTypeController : CRUDControllerBase<LessonType>
    {

        private readonly ILessonTypeService _lessonTypeService;
        private readonly IMapper _mapper;

        public LessonTypeController(ILessonTypeService service, IMapper mapper)
        : base(service)
        {
            _lessonTypeService = service;
            _mapper = mapper;
        }

        [HttpPost("search")]
        public async Task<ActionResult<PaginationDto<LessonTypeDto>>> Search(SearchDto searchDto)
        {
            try
            {
                var result = await _lessonTypeService.Search(searchDto);
                return Ok(new PaginationDto<LessonTypeDto>
                {
                    PageNumber = result.PageNumber,
                    PageSize = result.PageSize,
                    TotalPages = result.TotalPages,
                    Entities = result.Entities.Select(x => _mapper.Map<LessonTypeDto>(x)).ToList()
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
