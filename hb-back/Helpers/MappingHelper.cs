using AutoMapper;
using BackendBase.Models;

namespace BackendBase.Helpers
{
    public class MappingHelper<TEntity, TEntityDto>
    {
        private readonly IMapper _mapper;

        public MappingHelper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TEntityDto toDto(TEntity entity)
        {
            return _mapper.Map<TEntityDto>(entity);
        }

        public ICollection<TEntityDto> toDto(ICollection<TEntity> entities)
        {
            return (ICollection<TEntityDto>)(entities.Select(x => toDto(x)));
        }

        public PaginationDto<TEntityDto> paginationToDto(PaginationDto<TEntity> pagination)
        {
            PaginationDto<TEntityDto> result = new PaginationDto<TEntityDto>
            {
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize,
                TotalPages = pagination.TotalPages,
                Entities = toDto(pagination.Entities)
            };
            return result;
        }
    }
}
