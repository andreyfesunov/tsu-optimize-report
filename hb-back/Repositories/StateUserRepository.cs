using AutoMapper;
using BackendBase.Data;
using BackendBase.Dto.CreateDto;
using BackendBase.Dto.Report;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Repositories
{
    public class StateUserRepository : BaseRepositoryV2<StateUser, ReportListDto>
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;


        public StateUserRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<StateUser> AddStateWithCreateDto(StateUserCreateDto stateUserCreateDto)
        {
            var stateUser = _mapper.Map<StateUser>(stateUserCreateDto);
            stateUser.Rate = 1.0;
            stateUser.Status = "ACTIVE";
            var model = await _context.AddAsync(stateUser);
            await Save();
            return model.Entity;
        }

        //public async Task<bool> ExistStateUser(StateUserCreateDto stateUserCreateDto)
        //{
        //    return _context.Set<StateUser>().FirstOrDefault(x => x.StateId == Guid.Parse(stateUserCreateDto.StateId) && x.UserId == Guid.Parse(stateUserCreateDto.UserId)) == null;
        //}

        protected override IQueryable<StateUser> IncludeChildren(IQueryable<StateUser> query)
        {
            return query.Include(x => x.Events)
                .Include(x => x.User)
                .Include(x => x.Files)
                .Include(x => x.Records)
                .Include(x => x.State)
                .ThenInclude(x => x.Job)
                .Include(x => x.State)
                .ThenInclude(x => x.Department)
                .ThenInclude(x => x.Institute);
        }
    }
}
