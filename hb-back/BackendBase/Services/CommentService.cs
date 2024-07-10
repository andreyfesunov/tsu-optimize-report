using AutoMapper;
using BackendBase.Dto;
using BackendBase.Dto.Comment;
using BackendBase.Helpers;
using BackendBase.Interfaces.Repositories.Common;
using BackendBase.Interfaces.Services;
using BackendBase.Models;
using BackendBase.Repositories;

namespace BackendBase.Services;

public class CommentService : ICommentService
{
    protected readonly IMapper _mapper;
    protected MappingHelper<Comment, CommentDto> _mappingHelper;
    protected IBaseRepository<Comment> _repository;

    public CommentService(CommentRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
        _mappingHelper = new MappingHelper<Comment, CommentDto>(_mapper);
    }

    public async Task<Comment> AddEntity(Comment entity)
    {
        return await _repository.AddEntity(entity);
    }

    public async Task<CommentDto> GetById(Guid id)
    {
        return _mappingHelper.ToDto(await _repository.GetById(id));
    }

    public async Task<ICollection<CommentDto>> GetAll()
    {
        return _mappingHelper.ToDto(await _repository.GetAll());
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
        return _mappingHelper.ToDto(await _repository.Search(searchDto));
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