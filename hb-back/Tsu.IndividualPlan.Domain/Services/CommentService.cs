using Tsu.IndividualPlan.Domain.Dto.Comment;
using Tsu.IndividualPlan.Domain.Interfaces.Repositories;
using Tsu.IndividualPlan.Domain.Interfaces.SecurityServices;
using Tsu.IndividualPlan.Domain.Interfaces.Services;
using Tsu.IndividualPlan.Domain.Models.Business;

namespace Tsu.IndividualPlan.Domain.Services;

public class CommentService(ICommentRepository repository, ICommentSecurityService security)
    : ICommentService
{
    public async Task<Comment> Update(CommentUpdateDto dto)
    {
        var comment = await repository.GetById(dto.Id);
        await security.validateCanUse(comment);
        // ****

        comment.Content = dto.Content;
        comment.FactDate = dto.FactDate;
        comment.PlanDate = dto.PlanDate;

        return await repository.UpdateEntity(comment);
    }

    public async Task<bool> DeleteById(Guid id)
    {
        var comment = await repository.GetById(id);
        await security.validateCanUse(comment);
        // ****

        return await repository.Delete(comment);
    }

    public async Task<Comment> AddEntity(CommentCreateDto dto)
    {
        var entity = new Comment(
            EventId: dto.EventId,
            Content: dto.Content,
            PlanDate: dto.PlanDate,
            FactDate: dto.FactDate
        );

        await security.validateCanUse(entity);
        await security.validateCanCreate(entity);
        // ****

        return await repository.AddEntity(entity);
    }
}