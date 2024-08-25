namespace Tsu.IndividualPlan.WebApi.Dto;

public class DepartmentDto
{
    public DepartmentDto(Guid Id, string Name, InstituteDto? Institute)
    {
        this.Id = Id;
        this.Name = Name;
        this.Institute = Institute;
    }

    public Guid Id { get; init; }
    public string Name { get; init; }
    public InstituteDto? Institute { get; init; }
}