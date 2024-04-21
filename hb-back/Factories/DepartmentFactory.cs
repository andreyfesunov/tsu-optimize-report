using BackendBase.Models;

namespace BackendBase.Factories;

public static class DepartmentFactory
{
    public static List<Department> Make(Institute institute)
    {
        return new List<Department>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Institute = institute,
                Name = "Вычислительная техника"
            }
        };
    }
}