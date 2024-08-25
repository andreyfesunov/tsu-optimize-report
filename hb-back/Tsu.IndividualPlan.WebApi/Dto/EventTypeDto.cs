namespace Tsu.IndividualPlan.WebApi.Dto;

public class EventTypeDto
{
    public EventTypeDto(Guid Id, string Name, string Description, WorkDto? Work)
    {
        this.Id = Id;
        this.Name = Name;
        this.Description = Description;
        this.Work = Work;
    }

    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public WorkDto? Work { get; init; }
}