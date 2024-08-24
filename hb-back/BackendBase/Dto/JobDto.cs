namespace BackendBase.Dto;

public class JobDto
{
    public JobDto(Guid Id, string Name)
    {
        this.Id = Id;
        this.Name = Name;
    }

    public Guid Id { get; init; }
    public string Name { get; init; }
}
