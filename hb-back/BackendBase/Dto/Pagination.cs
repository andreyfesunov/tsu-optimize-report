namespace BackendBase.Models;

public class Pagination<TEntity>
{
    public Pagination(
        int PageNumber,
        int PageSize,
        ICollection<TEntity> Entities,
        int CollectionLength
    )
    {
        this.PageNumber = PageNumber;
        this.PageSize = PageSize;
        this.Entities = Entities;
        this.TotalPages = CollectionLength / PageSize + (CollectionLength % PageSize != 0 ? 1 : 0);
    }

    public Pagination(int PageNumber, int PageSize, int TotalPages, ICollection<TEntity> Entities)
    {
        this.PageNumber = PageNumber;
        this.PageSize = PageSize;
        this.TotalPages = TotalPages;
        this.Entities = Entities;
    }

    public readonly int PageNumber;
    public readonly int PageSize;
    public readonly int TotalPages;
    public readonly ICollection<TEntity> Entities;
}
