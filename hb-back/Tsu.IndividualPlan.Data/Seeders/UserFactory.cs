using Tsu.IndividualPlan.Domain.Enumerations;
using Tsu.IndividualPlan.Domain.Models.Business;
using Tsu.IndividualPlan.Domain.Utils;

namespace Tsu.IndividualPlan.Data.Seeders;

public static class UserFactory
{
    public static List<User> Make()
    {
        return
        [
            new User(
                "test1@gmail.com",
                Firstname: "Юлия",
                Lastname: "Французова",
                Password: PasswordUtils.GetPasswordHash("123123"),
                Role: RoleUserEnum.Admin
            ),

            new User(
                "test2@gmail.com",
                Firstname: "Марина",
                Lastname: "Андриянова",
                Password: PasswordUtils.GetPasswordHash("123123"),
                Role: RoleUserEnum.Admin
            ),

            new User(
                "test3@gmail.com",
                Firstname: "Антон",
                Lastname: "Гладких",
                Password: PasswordUtils.GetPasswordHash("123123"),
                Role: RoleUserEnum.User
            ),

            new User(
                "test4@gmail.com",
                Firstname: "Анастасия",
                Lastname: "Демидова",
                Password: PasswordUtils.GetPasswordHash("123123"),
                Role: RoleUserEnum.User
            )
        ];
    }
}