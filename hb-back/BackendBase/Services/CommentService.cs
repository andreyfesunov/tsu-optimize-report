using AutoMapper;
using BackendBase.Dto;
using BackendBase.Dto.Comment;
using BackendBase.Helpers;
using BackendBase.Interfaces.Services;
using BackendBase.Models;
using BackendBase.Repositories;

namespace BackendBase.Services;

public class CommentService : ICommentService
{
    protected readonly IMapper _mapper;
    protected ICommentRepository _repository;

    public CommentService(ICommentRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Comment> AddEntity(Comment entity)
    {
        return await _repository.AddEntity(entity);
    }

    public async Task<CommentDto> GetById(Guid id)
    {
        return _mapper.Map<CommentDto>(await _repository.GetById(id));
    }

    public async Task<ICollection<CommentDto>> GetAll()
    {
        return (await _repository.GetAll()).Select(u => _mapper.Map<CommentDto>(u)).ToList();
    }

    public async Task<Comment> Update(Comment entity)
    {
        return await _repository.UpdateEntity(entity);
    }

    public async Task<bool> DeleteById(Guid entityId)
    {
        return await _repository.DeleteById(entityId);
    }

    public async Task<Pagination<CommentDto>> Search(SearchDto searchDto)
    {
        var result = await _repository.Search(searchDto);
        return new Pagination<CommentDto>
        {
            PageNumber = result.PageNumber,
            PageSize = result.PageSize,
            TotalPages = result.TotalPages,
            Entities = result.Entities.Select(u => _mapper.Map<CommentDto>(u)).ToList()
        };
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