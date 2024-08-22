using System.Diagnostics.CodeAnalysis;

namespace BackendBase.Models;

public class Degree : Base
{
    protected Degree() { }

    [SetsRequiredMembers]
    public Degree(string Name, Guid? Id = null)
        : base(Id)
    {
        this.Name = Name;
    }

    public required string Name { get; init; }
    public ICollection<User>? Users { get; init; }
}
