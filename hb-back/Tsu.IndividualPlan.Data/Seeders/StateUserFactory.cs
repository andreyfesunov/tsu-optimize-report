using Tsu.IndividualPlan.Domain.Models.Business;

namespace Tsu.IndividualPlan.Data.Seeders;

public static class StateUserFactory
{
    public static List<StateUser> Make(User user, State state)
    {
        return [new StateUser(Rate: 1.0, UserId: user.Id, StateId: state.Id)];
    }
}