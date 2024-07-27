using BackendBase.Dto.Comment;
using BackendBase.Models;

namespace BackendBase.Interfaces.Services;

public interface ICommentService
{
    Task<Comment> AddEntity(Comment entity);
    Task<Comment> Update(CommentUpdateDto entity);
    Task<bool> DeleteById(Guid entityId);
}
