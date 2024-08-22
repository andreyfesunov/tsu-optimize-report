namespace BackendBase.Dto;

public class WorkDto
{
    public WorkDto(Guid Id, string Name)
    {
        this.Id = Id;
        this.Name = Name;
    }

    public readonly Guid Id;
    public readonly string Name;
}
