namespace Tsu.IndividualPlan.Domain.Models.Project;

public class Search(int PageNumber, int PageSize)
{
    public int PageNumber { get; init; } = PageNumber;
    public int PageSize { get; init; } = PageSize;
}