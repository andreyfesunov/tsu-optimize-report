using System.Diagnostics.CodeAnalysis;

namespace Tsu.IndividualPlan.Domain.Models.Business;

public class File : Base
{
    protected File()
    {
    }

    [SetsRequiredMembers]
    public File(Guid StateUserId, string Path, DateTime CreatedDate, Guid? Id = null)
        : base(Id)
    {
        this.StateUserId = StateUserId;
        this.Path = Path;
        this.CreatedDate = CreatedDate;
    }

    public required Guid StateUserId { get; init; }
    public required string Path { get; init; }
    public required DateTime CreatedDate { get; init; }

    public StateUser? StateUser { get; init; }
}