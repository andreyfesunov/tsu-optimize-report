using BackendBase.Dto.Comment;
using BackendBase.Models;

namespace BackendBase.Interfaces.Services;

public interface ICommentService
{
    Task<Comment> AddEntity(CommentCreateDto dto);
    Task<Comment> Update(CommentUpdateDto dto);
    Task<bool> DeleteById(Guid entityId);
}
