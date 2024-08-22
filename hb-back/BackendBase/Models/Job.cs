using System.Diagnostics.CodeAnalysis;

namespace BackendBase.Models;

public class Job : Base
{
    protected Job() { }

    [SetsRequiredMembers]
    public Job(string Name, Guid? Id = null)
        : base(Id) => this.Name = Name;

    public required string Name { get; init; }

    public ICollection<State>? States { get; init; }
}
