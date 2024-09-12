using Tsu.IndividualPlan.Domain.Dto.Comment;
using Tsu.IndividualPlan.Domain.Models.Business;

namespace Tsu.IndividualPlan.Domain.Interfaces.Services;

public interface ICommentService
{
    Task<Comment> AddEntity(CommentCreateDto dto);
    Task<Comment> Update(CommentUpdateDto dto);
    Task<bool> DeleteById(Guid entityId);
}