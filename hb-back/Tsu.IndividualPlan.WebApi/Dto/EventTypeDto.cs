namespace Tsu.IndividualPlan.WebApi.Dto;

public class EventTypeDto(Guid Id, string Name, string Description, WorkDto? Work)
{
    public Guid Id { get; init; } = Id;
    public string Name { get; init; } = Name;
    public string Description { get; init; } = Description;
    public WorkDto? Work { get; init; } = Work;
}