namespace BackendBase.Dto;

public class DegreeDto
{
    public DegreeDto(Guid Id, string Name)
    {
        this.Id = Id;
        this.Name = Name;
    }

    public Guid Id { get; init; }
    public string Name { get; init; }
}
