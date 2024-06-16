using BackendBase.Data;
using BackendBase.Models;

namespace BackendBase.Repositories;

public class CommentRepository : BaseRepository<Comment>
{
    public CommentRepository(DataContext context) : base(context)
    {
    }
}