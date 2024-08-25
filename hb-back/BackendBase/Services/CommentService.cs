using BackendBase.Dto.Comment;
using BackendBase.Interfaces.Repositories;
using BackendBase.Interfaces.SecurityServices;
using BackendBase.Interfaces.Services;
using BackendBase.Models;

namespace BackendBase.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _repository;
    private readonly ICommentSecurityService _security;

    public CommentService(ICommentRepository repository, ICommentSecurityService security)
    {
        _repository = repository;
        _security = security;
    }

    public async Task<Comment> Update(CommentUpdateDto dto)
    {
        var comment = await _repository.GetById(dto.Id);
        await _security.validateCanUse(comment);
        // ****

        comment.Content = dto.Content;
        comment.FactDate = dto.FactDate;
        comment.PlanDate = dto.PlanDate;

        return await _repository.UpdateEntity(comment);
    }

    public async Task<bool> DeleteById(Guid id)
    {
        var comment = await _repository.GetById(id);
        await _security.validateCanUse(comment);
        // ****

        return await _repository.Delete(comment);
    }

    public async Task<Comment> AddEntity(CommentCreateDto dto)
    {
        var entity = new Comment(
            EventId: dto.EventId,
            Content: dto.Content,
            PlanDate: dto.PlanDate,
            FactDate: dto.FactDate
        );

        await _security.validateCanUse(entity);
        await _security.validateCanCreate(entity);
        // ****

        return await _repository.AddEntity(entity);
    }
}
