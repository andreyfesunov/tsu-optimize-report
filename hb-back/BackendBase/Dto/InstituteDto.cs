namespace BackendBase.Dto;

public class InstituteDto
{
    public InstituteDto(Guid Id, string Name)
    {
        this.Id = Id;
        this.Name = Name;
    }

    public Guid Id { get; init; }
    public string Name { get; init; }
}
