using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using Tsu.IndividualPlan.Domain.Dto.Auth;
using Tsu.IndividualPlan.Domain.Enumerations;
using Tsu.IndividualPlan.Domain.Interfaces.Repositories;
using Tsu.IndividualPlan.Domain.Interfaces.Services;
using Tsu.IndividualPlan.Domain.Models.Business;
using Tsu.IndividualPlan.Domain.Services;
using Tsu.IndividualPlan.Domain.Utils;

namespace Tsu.IndividualPlan.Tests.Services;

[TestFixture]
public class UserServiceTests
{
    [OneTimeSetUp]
    public void InitConfiguration()
    {
        _configuration["Jwt:Audience"] = "localhost:4200";
        _configuration["Jwt:Issuer"] = "localhost:4200";
        _configuration["Jwt:Key"] = "tgthVABOl50mLjZOPRz2Y1qBquczBAxH";
    }

    [SetUp]
    public void MockReset()
    {
        _repository.Reset();
    }

    public UserServiceTests()
    {
        _service = new UserService(_configuration, _repository.Object);
    }

    private readonly ConfigurationManager _configuration = new();
    private readonly Mock<IUserRepository> _repository = new();
    private readonly IUserService _service;

    [Test]
    public async Task REGISTER_NewUser()
    {
        var dto = new RegistrationDto(
            "test1@gmail.com",
            Firstname: "Alex",
            Lastname: "Shuverov",
            Password: "123123"
        );

        _repository.Setup(x => x.GetByEmail("test1@gmail.com")).ReturnsAsync((User?)null);
        _repository.Setup(x => x.AddEntity(It.IsAny<User>())).ReturnsAsync((User x) => x);

        var role = await _service.Reg(dto);

        Assert.AreEqual(RoleUserEnum.User, role);
    }

    [Test]
    public void REGISTER_UserExists()
    {
        var dto = new RegistrationDto(
            "test1@gmail.com",
            Firstname: "Alex",
            Lastname: "Shuverov",
            Password: "123123"
        );

        _repository
            .Setup(x => x.GetByEmail("test1@gmail.com"))
            .ReturnsAsync(
                new User(
                    "test1@gmail.com",
                    "Password",
                    RoleUserEnum.User,
                    "Firstname",
                    "Lastname"
                )
            );

        Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await _service.Reg(dto));
    }

    [Test]
    public async Task LOGIN_Success()
    {
        var dto = new LoginDto("test1@gmail.com", "123123");

        _repository
            .Setup(x => x.GetByEmail("test1@gmail.com"))
            .ReturnsAsync(
                new User(
                    "test1@gmail.com",
                    PasswordUtils.GetPasswordHash("123123"),
                    RoleUserEnum.User,
                    "Firstname",
                    "Lastname"
                )
            );

        await _service.LogIn(dto);

        Assert.Pass("No errors was thrown");
    }

    [Test]
    public void LOGIN_NotFound()
    {
        var dto = new LoginDto("test1@gmail.com", "123123");

        _repository.Setup(x => x.GetByEmail("test1@gmail.com")).ReturnsAsync((User?)null);

        Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await _service.LogIn(dto));
    }

    [Test]
    public void LOGIN_WrongPassword()
    {
        var dto = new LoginDto("test1@gmail.com", "123123");

        _repository
            .Setup(x => x.GetByEmail("test1@gmail.com"))
            .ReturnsAsync(
                new User(
                    "test1@gmail.com",
                    PasswordUtils.GetPasswordHash("123456"),
                    RoleUserEnum.User,
                    "Firstname",
                    "Lastname"
                )
            );

        Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await _service.LogIn(dto));
    }
}