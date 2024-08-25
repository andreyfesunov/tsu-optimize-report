using System.Diagnostics.CodeAnalysis;
using BackendBase.Exceptions;
using BackendBase.Models.Enum;

namespace BackendBase.Models;

public class StateUser : Base
{
    protected StateUser() { }

    [SetsRequiredMembers]
    public StateUser(Guid StateId, Guid UserId, double Rate, Guid? Id = null)
        : base(Id)
    {
        this.StateId = StateId;
        this.UserId = UserId;
        this.Rate = Rate;
    }

    public required Guid StateId { get; init; }
    public required Guid UserId { get; init; }
    public required double Rate { get; init; }

    public ICollection<Event>? Events { get; init; }
    public ICollection<File>? Files { get; init; }
    public ICollection<Record>? Records { get; init; }

    public State? State { get; init; }
    public User? User { get; init; }

    public StateUserStatus Status
    {
        get
        {
            if (this.State == null)
            {
                throw new AppException("State is not loaded");
            }
            if (this.State.EndDate <= DateTime.Now)
            {
                return StateUserStatus.Finished;
            }
            if (this.Records == null)
            {
                throw new AppException("Records are not loaded");
            }
            if (this.Records.Count > 0)
            {
                return StateUserStatus.Active;
            }
            return StateUserStatus.NotActive;
        }
    }
}
