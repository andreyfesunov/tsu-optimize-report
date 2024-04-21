using BackendBase.Models;

namespace BackendBase.Factories;

public static class DepartmentFactory
{
    public static List<Department> Make(Institute institute)
    {
        var departments = new List<Department>
        {
            new Department
            {
                Id = Guid.NewGuid(),
                Insitute = institute,
                Name = "Вычислительная техника"
            }
        };
        
        return departments;
    }
}