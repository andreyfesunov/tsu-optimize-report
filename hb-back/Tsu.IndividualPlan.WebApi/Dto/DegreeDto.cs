namespace Tsu.IndividualPlan.WebApi.Dto;

public class DegreeDto(Guid Id, string Name)
{
    public Guid Id { get; init; } = Id;
    public string Name { get; init; } = Name;
}