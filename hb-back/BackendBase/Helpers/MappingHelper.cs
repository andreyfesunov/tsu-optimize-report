using AutoMapper;
using BackendBase.Models;

namespace BackendBase.Helpers;

public class MappingHelper<TEntity, TEntityDto>
{
    private readonly IMapper _mapper;

    public MappingHelper(IMapper mapper)
    {
        _mapper = mapper;
    }

    public TEntityDto ToDto(TEntity entity)
    {
        return _mapper.Map<TEntityDto>(entity);
    }

    public ICollection<TEntityDto> ToDto(IEnumerable<TEntity> entities)
    {
        return entities.Select(ToDto).ToList();
    }

    public Pagination<TEntityDto> ToDto(Pagination<TEntity> pagination)
    {
        return new Pagination<TEntityDto>
        {
            PageNumber = pagination.PageNumber,
            PageSize = pagination.PageSize,
            TotalPages = pagination.TotalPages,
            Entities = ToDto(pagination.Entities)
        };
    }
}