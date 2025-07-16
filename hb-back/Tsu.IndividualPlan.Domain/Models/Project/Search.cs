namespace Tsu.IndividualPlan.Domain.Models.Project;

public class Search(int PageNumber, int PageSize, bool IsActive = true)
{
    public int PageNumber { get; init; } = PageNumber;
    public int PageSize { get; init; } = PageSize;
    public bool IsActive { get; init; } = IsActive;
}