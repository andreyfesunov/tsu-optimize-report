using System.Diagnostics.CodeAnalysis;

namespace Tsu.IndividualPlan.Domain.Models.Business;

public class Rank : Base
{
    protected Rank()
    {
    }

    [SetsRequiredMembers]
    public Rank(string Name, Guid? Id = null)
        : base(Id)
    {
        this.Name = Name;
    }

    public required string Name { get; init; }

    public ICollection<User>? Users { get; init; }
}