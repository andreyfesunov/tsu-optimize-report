using System.Diagnostics.CodeAnalysis;

namespace Tsu.IndividualPlan.WebApi.Models;

public class Department : Base
{
    protected Department()
    {
    }

    [SetsRequiredMembers]
    public Department(string Name, Guid InstituteId, Guid? Id = null)
    {
        this.Name = Name;
        this.InstituteId = InstituteId;
    }

    public required string Name { get; init; }
    public required Guid InstituteId { get; init; }

public ICollection<State>? States { get; init; }
public Institute? Institute { get; init; }
}