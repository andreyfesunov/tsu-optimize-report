using System;
using System.Threading.Tasks;
using BackendBase.Dto;
using BackendBase.Exceptions;
using BackendBase.Interfaces.Repositories;
using BackendBase.Interfaces.SecurityServices;
using BackendBase.Models;
using BackendBase.SecurityServices;
using Moq;
using NUnit.Framework;

namespace TsuImportTests.SecurityServices;

[TestFixture]
public class CommentSecurityServiceTests
{
    public CommentSecurityServiceTests()
    {
        _service = new CommentSecurityService(
                _userInfo.Object,
                _stateUserRepo.Object,
                _eventRepo.Object
                );
    }

    private readonly ICommentSecurityService _service;
    private readonly Mock<UserInfo> _userInfo = new();
    private readonly Mock<IStateUserRepository> _stateUserRepo = new();
    private readonly Mock<IEventRepository> _eventRepo = new();

    [SetUp]
    public void MockReset()
    {
        _stateUserRepo.Reset();
        _userInfo.Reset();
        _eventRepo.Reset();
    }

    [Test]
    public async Task COMMENT_CanUse__VALID()
    {
        var userId = Guid.NewGuid();

        _userInfo.Setup(x => x.GetUserId()).Returns(userId.ToString());
        _eventRepo.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(new Event
        {
            StateUserId = Guid.NewGuid()
        });
        _stateUserRepo.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(new StateUser
        {
            UserId = userId
        });

        await _service.validateCanUse(new Comment { EventId = Guid.NewGuid() });

        Assert.Pass("Can use");
    }

    [Test]
    public void COMMENT_CanUse__NotOwner()
    {
        _userInfo.Setup(x => x.GetUserId()).Returns(Guid.NewGuid().ToString());
        _eventRepo.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(new Event
        {
            StateUserId = Guid.NewGuid()
        });
        _stateUserRepo.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(new StateUser
        {
            UserId = Guid.NewGuid()
        });

        Assert.ThrowsAsync<AppException>(async () => await _service.validateCanUse(new Comment { EventId = Guid.NewGuid() }));
    }

    [Test]
    public async Task COMMENT_CanCreate__VALID()
    {
        _eventRepo.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(new Event
        {
            EventType = new EventType { WorkId = Guid.Parse(SystemWorks.ExtraWorkId) },
        });

        await _service.validateCanCreate(new Comment { EventId = Guid.NewGuid() });

        Assert.Pass("Can create");
    }

    [Test]
    public void COMMENT_CanCreate__IncorrectWorkId()
    {
        _eventRepo.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(new Event
        {
            EventType = new EventType { WorkId = Guid.Parse(SystemWorks.AcademicMethodicalWorkId) },
        });

        Assert.ThrowsAsync<AppException>(async () => await _service.validateCanCreate(new Comment { EventId = Guid.NewGuid() }));
    }
}
