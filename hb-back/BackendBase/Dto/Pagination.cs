namespace BackendBase.Models;

public class Pagination<TEntity>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public ICollection<TEntity> Entities { get; set; }
}