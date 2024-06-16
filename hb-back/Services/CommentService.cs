using AutoMapper;
using BackendBase.Dto;
using BackendBase.Dto.Comment;
using BackendBase.Helpers.CRUD;
using BackendBase.Interfaces;
using BackendBase.Models;
using BackendBase.Repositories;

namespace BackendBase.Services;

public class CommentService : CRUDServiceBase<Comment, CommentDto>, ICommentService
{
    public CommentService(CommentRepository repository, IMapper mapper) : base(mapper)
    {
        _repository = repository;
    }

    public async Task<CommentDto> Update(CommentUpdateDto dto)
    {
        var comment = await _repository.GetById(Guid.Parse(dto.Id));

        comment.Content = dto.Content;
        comment.FactDate = dto.FactDate;
        comment.PlanDate = dto.PlanDate;

        comment = await _repository.UpdateEntity(comment);

        return _mapper.Map<CommentDto>(comment);
    }
}