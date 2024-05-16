using AutoMapper;
using BackendBase.Data;
using BackendBase.Dto;
using BackendBase.Dto.CreateDto;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Repositories
{
    public class StateRepository : BaseRepositoryV2<State, StateDto>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly DepartmentRepository _departmentRepository;

        public StateRepository(DataContext context, IMapper mapper, DepartmentRepository departmentRepository) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
            _departmentRepository = departmentRepository;
        }

        public async Task<State> AddStateWithCreateDto(StateCreateDto stateCreateDto)
        {
            var state = _mapper.Map<State>(stateCreateDto);
            var test = _departmentRepository.Search(new SearchDto() { PageNumber = 1, PageSize = 1 });
            state.DepartmentId = Guid.Parse(test.Result.Entities.First().Id);
            var model = await _context.AddAsync(state);
            await Save();
            return model.Entity;
        }

        protected override IQueryable<State> IncludeChildren(IQueryable<State> query)
        {
            return query
                    .Include(x => x.Department)
                    .ThenInclude(x => x.Institute)
                    .Include(x => x.Job);
        }
    }
}
