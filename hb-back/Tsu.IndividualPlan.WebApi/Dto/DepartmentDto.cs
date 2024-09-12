namespace Tsu.IndividualPlan.WebApi.Dto;

public class DepartmentDto(Guid Id, string Name, InstituteDto? Institute)
{
    public Guid Id { get; init; } = Id;
    public string Name { get; init; } = Name;
    public InstituteDto? Institute { get; init; } = Institute;
}