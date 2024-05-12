namespace BackendBase.Dto;

public class DepartmentDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public InstituteDto Institute { get; set; }
}