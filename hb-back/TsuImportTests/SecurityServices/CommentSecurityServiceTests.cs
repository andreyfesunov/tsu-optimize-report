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
        _eventRepo
            .Setup(x => x.GetById(It.IsAny<Guid>()))
            .ReturnsAsync(
                new Event(
                    StateUserId: Guid.NewGuid(),
                    EventTypeId: Guid.NewGuid(),
                    StartedAt: DateTime.UtcNow,
                    EndedAt: DateTime.UtcNow
                )
            );
        _stateUserRepo
            .Setup(x => x.GetById(It.IsAny<Guid>()))
            .ReturnsAsync(new StateUser(UserId: userId, StateId: Guid.NewGuid(), Rate: 1.0));

        await _service.validateCanUse(new Comment(EventId: Guid.NewGuid(), Content: "Content"));

        Assert.Pass("Can use");
    }

    [Test]
    public void COMMENT_CanUse__NotOwner()
    {
        _userInfo.Setup(x => x.GetUserId()).Returns(Guid.NewGuid().ToString());
        _eventRepo
            .Setup(x => x.GetById(It.IsAny<Guid>()))
            .ReturnsAsync(
                new Event(
                    StateUserId: Guid.NewGuid(),
                    StartedAt: DateTime.UtcNow,
                    EndedAt: DateTime.UtcNow,
                    EventTypeId: Guid.NewGuid()
                )
            );
        _stateUserRepo
            .Setup(x => x.GetById(It.IsAny<Guid>()))
            .ReturnsAsync(
                new StateUser(UserId: Guid.NewGuid(), StateId: Guid.NewGuid(), Rate: 1.0)
            );

        Assert.ThrowsAsync<AppException>(
            async () =>
                await _service.validateCanUse(
                    new Comment(EventId: Guid.NewGuid(), Content: "Content")
                )
        );
    }

    [Test]
    public async Task COMMENT_CanCreate__VALID()
    {
        var entity = new Mock<Event>();

        entity.Setup(x => x.WorkId).Returns(Guid.Parse(SystemWorks.ExtraWorkId));

        _eventRepo.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(entity.Object);

        await _service.validateCanCreate(new Comment(EventId: Guid.NewGuid(), Content: "Content"));

        Assert.Pass("Can create");
    }

    [Test]
    public void COMMENT_CanCreate__IncorrectWorkId()
    {
        var entity = new Mock<Event>();

        entity.Setup(x => x.WorkId).Returns(Guid.Parse(SystemWorks.AcademicMethodicalWorkId));

        _eventRepo.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(entity.Object);

        Assert.ThrowsAsync<AppException>(
            async () =>
                await _service.validateCanCreate(
                    new Comment(EventId: Guid.NewGuid(), Content: "Content")
                )
        );
    }
}
