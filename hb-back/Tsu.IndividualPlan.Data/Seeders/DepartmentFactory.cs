using Tsu.IndividualPlan.Domain.Models.Business;

namespace Tsu.IndividualPlan.Data.Seeders;

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