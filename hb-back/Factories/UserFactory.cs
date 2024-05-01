using BackendBase.Models;
using BackendBase.Utils;

namespace BackendBase.Factories;

public static class UserFactory
{
    public static List<User> Make()
    {
        return new List<User>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Email = "test1@gmail.com",
                Firstname = "Alexey",
                Lastname = "Petrov",
                Password = PasswordUtils.GetPasswordHash("123123")
            },
            new()
            {
                Id = Guid.NewGuid(),
                Email = "test2@gmail.com",
                Firstname = "Ivan",
                Lastname = "Petrov",
                Password = PasswordUtils.GetPasswordHash("123123"),
            },
        };
    }
}