using System.Diagnostics.CodeAnalysis;

namespace Tsu.IndividualPlan.WebApi.Models;

public class Degree : Base
{
    protected Degree()
    {
    }

    [SetsRequiredMembers]
    public Degree(string Name, Guid? Id = null)
        : base(Id)
    {
        this.Name = Name;
    }

    public required string Name { get; init; }
    public ICollection<User>? Users { get; init; }
}