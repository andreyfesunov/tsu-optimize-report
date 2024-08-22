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
public class LessonSecurityServiceTests
{
    public LessonSecurityServiceTests()
    {
        _service = new LessonSecurityService(
            _userInfo.Object,
            _stateUserRepo.Object,
            _eventRepo.Object
        );
    }

    private readonly ILessonSecurityService _service;
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
    public async Task LESSON_CanUse__VALID()
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

        await _service.validateCanUse(
            new Lesson(EventId: Guid.NewGuid(), LessonTypeId: Guid.NewGuid())
        );

        Assert.Pass("Can use");
    }

    [Test]
    public void LESSON_CanUse__NotOwner()
    {
        _userInfo.Setup(x => x.GetUserId()).Returns(Guid.NewGuid().ToString());
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
            .ReturnsAsync(
                new StateUser(UserId: Guid.NewGuid(), StateId: Guid.NewGuid(), Rate: 1.0)
            );

        Assert.ThrowsAsync<AppException>(
            async () =>
                await _service.validateCanUse(
                    new Lesson(EventId: Guid.NewGuid(), LessonTypeId: Guid.NewGuid())
                )
        );
    }

    [Test]
    public async Task LESSON_CanCreate__VALID()
    {
        var entity = new Mock<Event>();

        entity.Setup(x => x.WorkId).Returns(Guid.Parse(SystemWorks.AcademicMethodicalWorkId));

        _eventRepo.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(entity.Object);

        await _service.validateCanCreate(
            new Lesson(EventId: Guid.NewGuid(), LessonTypeId: Guid.NewGuid())
        );

        Assert.Pass("Can create");
    }

    [Test]
    public void LESSON_CanCreate__IncorrectWorkId()
    {
        var entity = new Mock<Event>();

        entity.Setup(x => x.WorkId).Returns(Guid.Parse(SystemWorks.ExtraWorkId));

        _eventRepo.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(entity.Object);

        Assert.ThrowsAsync<AppException>(
            async () =>
                await _service.validateCanCreate(
                    new Lesson(EventId: Guid.NewGuid(), LessonTypeId: Guid.NewGuid())
                )
        );
    }

    [Test]
    public void LESSON_CanCreate__LessonTypeCopy()
    {
        var id = Guid.NewGuid();
        var entity = new Mock<Event>();

        entity.Setup(x => x.HasLessonType(id)).Returns(true);
        _eventRepo.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(entity.Object);

        Assert.ThrowsAsync<AppException>(
            async () =>
                await _service.validateCanCreate(
                    new Lesson(EventId: Guid.NewGuid(), LessonTypeId: id)
                )
        );
    }
}
