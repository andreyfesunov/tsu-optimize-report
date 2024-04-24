using BackendBase.Data;
using BackendBase.Models;

namespace BackendBase.Repositories
{
    public class LessonRepository : BaseRepository<Lesson>
    {
        private readonly DataContext _context;

        public LessonRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
