using System.Diagnostics.CodeAnalysis;

namespace BackendBase.Models;

public class Rank : Base
{
    protected Rank() { }

    [SetsRequiredMembers]
    public Rank(string Name, Guid? Id = null)
        : base(Id) => this.Name = Name;

    public required string Name { get; init; }

    public ICollection<User>? Users { get; init; }
}
