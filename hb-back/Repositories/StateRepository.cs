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
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly DepartmentRepository _departmentRepository;
        private readonly UserRepository _userRepository;
        private readonly StateUserRepository _stateUserRepository;

        public StateRepository(
            IMapper mapper,
            DataContext context,
            DepartmentRepository departmentRepository,
            UserRepository userRepository,
            StateUserRepository stateUserRepository
            ) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
            _departmentRepository = departmentRepository;
            _userRepository = userRepository;
            _stateUserRepository = stateUserRepository;
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

        public async Task<bool> SetState(StateUserCreateDto stateUserCreate)
        {
            var state = await GetEntityById(Guid.Parse(stateUserCreate.StateId));
            var userDto = _userRepository.GetById(Guid.Parse(stateUserCreate.UserId));
            if (state == null || userDto == null) return false;

            if (state.Count < 1) return false;
            state.Count -= 1;
            await UpdateEntity(state);
            await _stateUserRepository.AddStateWithCreateDto(stateUserCreate);
            return true;
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
