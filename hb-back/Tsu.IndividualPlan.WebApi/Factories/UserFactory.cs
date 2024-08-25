using Tsu.IndividualPlan.WebApi.Models;
using Tsu.IndividualPlan.WebApi.Models.Enum;
using Tsu.IndividualPlan.WebApi.Utils;

namespace Tsu.IndividualPlan.WebApi.Factories;

public static class UserFactory
{
    public static List<User> Make()
    {
        return new List<User>
        {
            new(
                "test1@gmail.com",
                Firstname: "Юлия",
                Lastname: "Французова",
                Password: PasswordUtils.GetPasswordHash("123123"),
                Role: RoleUserEnum.Admin
            ),
            new(
                "test2@gmail.com",
                Firstname: "Марина",
                Lastname: "Андриянова",
                Password: PasswordUtils.GetPasswordHash("123123"),
                Role: RoleUserEnum.Admin
            ),
            new(
                "test3@gmail.com",
                Firstname: "Антон",
                Lastname: "Гладких",
                Password: PasswordUtils.GetPasswordHash("123123"),
                Role: RoleUserEnum.User
            ),
            new(
                "test4@gmail.com",
                Firstname: "Анастасия",
                Lastname: "Демидова",
                Password: PasswordUtils.GetPasswordHash("123123"),
                Role: RoleUserEnum.User
            )
        };
    }
}