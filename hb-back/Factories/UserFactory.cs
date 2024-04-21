using BackendBase.Models;

namespace BackendBase.Factories;

public class UserFactory
{
    public static IEnumerable<User> Make()
    {
        return new List<User>
        {
            new User
            {
                Id = Guid.NewGuid(),
                Email = "test1@gmail.com",
                Firstname = "Alexey",
                Lastname = "Petrov",
                Password = "123123"
            },
            new User
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