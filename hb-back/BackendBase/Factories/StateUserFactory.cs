using BackendBase.Models;

namespace BackendBase.Factories;

public static class StateUserFactory
{
    public static List<StateUser> Make(User user, State state)
    {
        return new List<StateUser>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Rate = 1.0,
                User = user,
                State = state
            }
        };
    }
}