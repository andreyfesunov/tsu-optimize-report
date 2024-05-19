using AutoMapper;
using BackendBase.Data;
using BackendBase.Dto;
using BackendBase.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendBase.Repositories
{
    public class LessonTypeRepository : BaseRepositoryV2<LessonType>
    {
        public LessonTypeRepository(DataContext context) : base(context)
        {
        }

        public Task<LessonType?> GetLessonTypeByName(string name)
        {
            return context.LessonTypes.Where(x => x.Name == name).FirstOrDefaultAsync();
        }

        protected override IQueryable<LessonType> IncludeChildren(IQueryable<LessonType> query)
        {
            return query;
        }
    }
}
