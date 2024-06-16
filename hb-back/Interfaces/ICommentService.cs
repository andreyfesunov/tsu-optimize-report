using BackendBase.Dto;
using BackendBase.Dto.Comment;
using BackendBase.Helpers.CRUD;
using BackendBase.Models;

namespace BackendBase.Interfaces;

public interface ICommentService : ICRUDServiceBase<Comment, CommentDto>
{
    Task<CommentDto> Update(CommentUpdateDto entity);
}