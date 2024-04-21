using BackendBase.Models;

namespace BackendBase.Factories;

public static class StateUserFactory
{
    public static IEnumerable<StateUser> Make(User user, State state)
    {
        return new List<StateUser>
        {
            new StateUser
            {
                Id = Guid.NewGuid(),
                Rate = 1.0,
                Status = "ACTIVE",
                User = user,
                State = state
            }
        };
    }
}