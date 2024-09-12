namespace Tsu.IndividualPlan.WebApi.Dto;

public class ActivityDto(Guid Id, string Name, int Column)
{
    public Guid Id { get; init; } = Id;
    public string Name { get; init; } = Name;
    public int Column { get; init; } = Column;
}