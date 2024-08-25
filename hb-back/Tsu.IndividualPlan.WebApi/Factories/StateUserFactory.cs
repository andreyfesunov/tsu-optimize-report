using Tsu.IndividualPlan.WebApi.Models;

namespace Tsu.IndividualPlan.WebApi.Factories;

public static class StateUserFactory
{
    public static List<StateUser> Make(User user, State state)
    {
        return new List<StateUser>() { new(Rate: 1.0, UserId: user.Id, StateId: state.Id) };
    }
}