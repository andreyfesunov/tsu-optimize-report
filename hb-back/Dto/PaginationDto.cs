namespace BackendBase.Models;

public class PaginationDto<TEntity>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public List<TEntity> Entities { get; set; }
}