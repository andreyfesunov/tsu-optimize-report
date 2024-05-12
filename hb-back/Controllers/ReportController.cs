﻿using AutoMapper;
using BackendBase.Dto;
using BackendBase.Dto.Report;
using BackendBase.Interfaces;
using BackendBase.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendBase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly IStateUserService _stateUserService;
        private readonly IMapper _mapper;

        public ReportController(IReportService reportService, IStateUserService stateUserService, IMapper mapper)
        {
            _reportService = reportService;
            _stateUserService = stateUserService;
            _mapper = mapper;
        }

        [HttpPost("{stateUserId:guid}/[action]")]
        public async Task<int> CreateReport(Guid stateUserId, IFormFile file)
        {
            return await _reportService.CreateReport(stateUserId, file);
        }

        [HttpPost("search")]
        public async Task<ActionResult<PaginationDto<ReportListDto>>> Search(SearchDto searchDto)
        {
            try
            {
                var result = await _stateUserService.Search(searchDto);
                return Ok(new PaginationDto<ReportListDto>
                {
                    PageNumber = result.PageNumber,
                    PageSize = result.PageSize,
                    TotalPages = result.TotalPages,
                    Entities = result.Entities.Select(x => _mapper.Map<ReportListDto>(x)).ToList()
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
