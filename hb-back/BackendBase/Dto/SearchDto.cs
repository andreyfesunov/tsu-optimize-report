namespace BackendBase.Dto;

public class SearchDto
{
    public SearchDto(int PageNumber, int PageSize)
    {
        this.PageNumber = PageNumber;
        this.PageSize = PageSize;
    }

    public readonly int PageNumber;
    public readonly int PageSize;
}
