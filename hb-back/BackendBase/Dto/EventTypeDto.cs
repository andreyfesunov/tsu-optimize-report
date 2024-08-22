namespace BackendBase.Dto;

public class EventTypeDto
{
    public EventTypeDto(Guid Id, string Name, string Description, WorkDto? Work)
    {
        this.Id = Id;
        this.Name = Name;
        this.Description = Description;
        this.Work = Work;
    }

    public readonly Guid Id;
    public readonly string Name;
    public readonly string Description;
    public readonly WorkDto? Work;
}
