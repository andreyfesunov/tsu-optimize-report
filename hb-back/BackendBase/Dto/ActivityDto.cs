namespace BackendBase.Dto;

public class ActivityDto
{
    public ActivityDto(Guid Id, string Name, int Column)
    {
        this.Id = Id;
        this.Name = Name;
        this.Column = Column;
    }

    public readonly Guid Id;
    public readonly string Name;
    public readonly int Column;
}
