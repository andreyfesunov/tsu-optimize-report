using BackendBase.Data;
using BackendBase.Models;

namespace BackendBase.Repositories
{
    public class LessonRepository : BaseRepository<Lesson>
    {
        public LessonRepository(DataContext context) : base(context)
        {
        }
    }
}
