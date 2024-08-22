using System.Diagnostics.CodeAnalysis;

namespace BackendBase.Models;

public class State : Base
{
    protected State() { }

    [SetsRequiredMembers]
    public State(
        Guid DepartmentId,
        Guid JobId,
        int Count,
        int Hours,
        DateTime StartDate,
        DateTime EndDate,
        Guid? Id = null
    )
        : base(Id)
    {
        this.DepartmentId = DepartmentId;
        this.JobId = JobId;
        this.Count = Count;
        this.Hours = Hours;
        this.StartDate = StartDate;
        this.EndDate = EndDate;
    }

    public required Guid DepartmentId { get; init; }
    public required Guid JobId { get; init; }
    public required int Hours { get; init; }
    public required DateTime StartDate { get; init; }
    public required DateTime EndDate { get; init; }

    /** Editable Fields */
    public required int Count { get; set; }

    public ICollection<StateUser>? StatesUsers { get; init; }

    public Department? Department { get; private set; }
    public Job? Job { get; private set; }
}
