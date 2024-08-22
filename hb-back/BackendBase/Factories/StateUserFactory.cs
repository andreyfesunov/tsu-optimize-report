using BackendBase.Models;

namespace BackendBase.Factories;

public static class StateUserFactory
{
    public static List<StateUser> Make(User user, State state) =>
        new List<StateUser> { new(Rate: 1.0, UserId: user.Id, StateId: state.Id) };
}
