using BackendBase.Models;
using BackendBase.Models.Enum;
using BackendBase.Utils;

namespace BackendBase.Factories;

public static class UserFactory
{
    public static List<User> Make()
    {
        return new List<User>
        {
            new(
                Email: "test1@gmail.com",
                Firstname: "Юлия",
                Lastname: "Французова",
                Password: PasswordUtils.GetPasswordHash("123123"),
                Role: RoleUserEnum.Admin
            ),
            new(
                Email: "test2@gmail.com",
                Firstname: "Марина",
                Lastname: "Андриянова",
                Password: PasswordUtils.GetPasswordHash("123123"),
                Role: RoleUserEnum.Admin
            ),
            new(
                Email: "test3@gmail.com",
                Firstname: "Антон",
                Lastname: "Гладких",
                Password: PasswordUtils.GetPasswordHash("123123"),
                Role: RoleUserEnum.User
            ),
            new(
                Email: "test4@gmail.com",
                Firstname: "Анастасия",
                Lastname: "Демидова",
                Password: PasswordUtils.GetPasswordHash("123123"),
                Role: RoleUserEnum.User
            )
        };
    }
}
