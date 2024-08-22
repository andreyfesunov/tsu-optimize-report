using BackendBase.Models;

namespace BackendBase.Factories;

public static class DepartmentFactory
{
    public static List<Department> Make(Institute institute)
    {
        return new List<Department>
        {
            new(InstituteId: institute.Id, Name: "Вычислительная техника")
        };
    }
}
