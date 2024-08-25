using Tsu.IndividualPlan.WebApi.Dto.Comment;
using Tsu.IndividualPlan.WebApi.Models;

namespace Tsu.IndividualPlan.WebApi.Interfaces.Services;

public interface ICommentService
{
    Task<Comment> AddEntity(CommentCreateDto dto);
    Task<Comment> Update(CommentUpdateDto dto);
    Task<bool> DeleteById(Guid entityId);
}