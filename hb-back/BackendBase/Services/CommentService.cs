using BackendBase.Dto.Comment;
using BackendBase.Interfaces.Services;
using BackendBase.Models;
using BackendBase.Repositories;

namespace BackendBase.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _repository;

    public CommentService(ICommentRepository repository)
    {
        _repository = repository;
    }

    public async Task<Comment> Update(CommentUpdateDto dto)
    {
        var comment = await _repository.GetById(Guid.Parse(dto.Id));

        comment.Content = dto.Content;
        comment.FactDate = dto.FactDate;
        comment.PlanDate = dto.PlanDate;

        return await _repository.UpdateEntity(comment);
    }

    public Task<bool> DeleteById(Guid id) => _repository.DeleteById(id);

    public Task<Comment> AddEntity(Comment entity) => _repository.AddEntity(entity); 
}
