using BackendBase.Dto;
using BackendBase.Dto.Comment;
using BackendBase.Models;

namespace BackendBase.Interfaces;

public interface ICommentService
{
    Task<Comment> AddEntity(Comment entity);
    Task<CommentDto> GetById(Guid id);
    Task<ICollection<CommentDto>> GetAll();
    Task<Comment> Update(Comment entity);
    Task<bool> DeleteById(Guid entityId);
    Task<PaginationDto<CommentDto>> Search(SearchDto searchDto);
    Task<CommentDto> Update(CommentUpdateDto entity);
}