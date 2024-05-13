using AutoMapper;
using BackendBase.Data;
using BackendBase.Dto;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Repositories
{
    public class LessonTypeRepository : BaseRepositoryV2<LessonType, LessonTypeDto>
    {
        private readonly DataContext _context;

        public LessonTypeRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
        }

        public Task<LessonType?> GetLessonTypeByName(string name)
        {
            return _context.LessonTypes.Where(x => x.Name == name).FirstOrDefaultAsync();
        }

        protected override IQueryable<LessonType> IncludeChildren(IQueryable<LessonType> query)
        {
            return query;
        }
    }
}
