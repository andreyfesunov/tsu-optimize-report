namespace BackendBase.Dto;

public class SearchDto
{
    public SearchDto(int PageNumber, int PageSize)
    {
        this.PageNumber = PageNumber;
        this.PageSize = PageSize;
    }

    public int PageNumber { get; init; }
    public int PageSize { get; init; }
}
