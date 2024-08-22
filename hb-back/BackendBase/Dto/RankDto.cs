namespace BackendBase.Dto;

public class RankDto
{
    public RankDto(Guid Id, string Name)
    {
        this.Id = Id;
        this.Name = Name;
    }

    public readonly Guid Id;
    public readonly string Name;
}
