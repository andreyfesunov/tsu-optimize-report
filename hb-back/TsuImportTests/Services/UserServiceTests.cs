using System;
using System.Threading.Tasks;
using BackendBase.Dto;
using BackendBase.Interfaces.Repositories;
using BackendBase.Interfaces.Services;
using BackendBase.Models;
using BackendBase.Models.Enum;
using BackendBase.Services;
using BackendBase.Utils;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;

namespace TsuImportTests.Services;

[TestFixture]
public class UserServiceTests
{
    public UserServiceTests()
    {
        _service = new UserService(_configuration, _repository.Object);
    }

    [OneTimeSetUp]
    public void InitConfiguration()
    {
        _configuration["Jwt:Audience"] = "localhost:4200";
        _configuration["Jwt:Issuer"] = "localhost:4200";
        _configuration["Jwt:Key"] = "TestJWTKey_TestJWTKey";
    }

    [SetUp]
    public void MockReset()
    {
        _repository.Reset();
    }

    private readonly ConfigurationManager _configuration = new();
    private readonly Mock<IUserRepository> _repository = new();
    private readonly IUserService _service;

    [Test]
    public async Task REGISTER_NewUser()
    {
        var dto = new RegistrationDto
        {
            Email = "test1@gmail.com",
            Firstname = "Alex",
            Lastname = "Shuverov",
            Password = "123123"
        };

        _repository.Setup(x => x.GetByEmail("test1@gmail.com")).ReturnsAsync((User?)null);
        _repository.Setup(x => x.AddEntity(It.IsAny<User>())).ReturnsAsync((User x) => x);

        var role = await _service.Reg(dto);

        Assert.AreEqual(RoleUserEnum.User, role);
    }

    [Test]
    public void REGISTER_UserExists()
    {
        var dto = new RegistrationDto
        {
            Email = "test1@gmail.com",
            Firstname = "Alex",
            Lastname = "Shuverov",
            Password = "123123"
        };

        _repository.Setup(x => x.GetByEmail("test1@gmail.com")).ReturnsAsync(new User
        {
            Email = "test1@gmail.com"
        });

        Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await _service.Reg(dto));
    }

    [Test]
    public async Task LOGIN_Success()
    {
        var dto = new LoginDto
        {
            Email = "test1@gmail.com",
            Password = "123123"
        };

        _repository.Setup(x => x.GetByEmail("test1@gmail.com")).ReturnsAsync(new User
        {
            Id = Guid.NewGuid(),
            Password = PasswordUtils.GetPasswordHash("123123"),
            Role = RoleUserEnum.User
        });

        await _service.LogIn(dto);

        Assert.Pass("No errors was thrown");
    }

    [Test]
    public void LOGIN_NotFound()
    {
        var dto = new LoginDto
        {
            Email = "test1@gmail.com",
            Password = "123123"
        };

        _repository.Setup(x => x.GetByEmail("test1@gmail.com")).ReturnsAsync((User?)null);

        Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await _service.LogIn(dto));
    }

    [Test]
    public void LOGIN_WrongPassword()
    {
        var dto = new LoginDto
        {
            Email = "test1@gmail.com",
            Password = "123123"
        };

        _repository.Setup(x => x.GetByEmail("test1@gmail.com")).ReturnsAsync(new User
        {
            Password = PasswordUtils.GetPasswordHash("123456")
        });

        Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await _service.LogIn(dto));
    }
}
