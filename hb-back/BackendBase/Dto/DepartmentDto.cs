namespace BackendBase.Dto;

public class DepartmentDto
{
    public DepartmentDto(Guid Id, string Name, InstituteDto? Institute)
    {
        this.Id = Id;
        this.Name = Name;
        this.Institute = Institute;
    }

    public readonly Guid Id;
    public readonly string Name;
    public readonly InstituteDto? Institute;
}
