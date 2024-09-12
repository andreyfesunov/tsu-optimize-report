namespace Tsu.IndividualPlan.Domain.Models.Project;

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
        TotalPages = CollectionLength / PageSize + (CollectionLength % PageSize != 0 ? 1 : 0);
    }

    public Pagination(int PageNumber, int PageSize, int TotalPages, ICollection<TEntity> Entities)
    {
        this.PageNumber = PageNumber;
        this.PageSize = PageSize;
        this.TotalPages = TotalPages;
        this.Entities = Entities;
    }

    public int PageNumber { get; init; }
    public int PageSize { get; init; }
    public int TotalPages { get; init; }
    public ICollection<TEntity> Entities { get; init; }
}