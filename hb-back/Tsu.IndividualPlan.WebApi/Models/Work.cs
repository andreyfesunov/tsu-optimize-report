using System.Diagnostics.CodeAnalysis;

namespace Tsu.IndividualPlan.WebApi.Models;

public class Work : Base
{
    protected Work()
    {
    }

    [SetsRequiredMembers]
    public Work(string Name, int Order, Guid? Id = null)
        : base(Id)
    {
        this.Name = Name;
        this.Order = Order;
    }

    public required string Name { get; init; }
    public required int Order { get; init; }
}