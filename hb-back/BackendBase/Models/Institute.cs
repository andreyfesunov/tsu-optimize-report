using System.Diagnostics.CodeAnalysis;

namespace BackendBase.Models;

public class Institute : Base
{
    protected Institute() { }

    [SetsRequiredMembers]
    public Institute(string Name, Guid? Id = null)
        : base(Id) => this.Name = Name;

    public required string Name { get; init; }

    public ICollection<Department>? Departments { get; init; }
}
