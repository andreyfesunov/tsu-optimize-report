using BackendBase.Models;

namespace BackendBase.Factories;

public class UserFactory
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
                Password = "123123"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Email = "test2@gmail.com",
                Firstname = "Ivan",
                Lastname = "Petrov",
                Password = "123123",
            },
        };
    }
}