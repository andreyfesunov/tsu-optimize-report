using System;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Tsu.IndividualPlan.WebApi.Dto;
using Tsu.IndividualPlan.WebApi.Exceptions;
using Tsu.IndividualPlan.WebApi.Interfaces.Repositories;
using Tsu.IndividualPlan.WebApi.Interfaces.SecurityServices;
using Tsu.IndividualPlan.WebApi.Models;
using Tsu.IndividualPlan.WebApi.SecurityServices;

namespace Tsu.IndividualPlan.Tests.SecurityServices;

[TestFixture]
public class LessonSecurityServiceTests
{
    [SetUp]
    public void MockReset()
    {
        _stateUserRepo.Reset();
        _userInfo.Reset();
        _eventRepo.Reset();
    }

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

    [Test]
    public async Task LESSON_CanUse__VALID()
    {
        var userId = Guid.NewGuid();

        _userInfo.Setup(x => x.GetUserId()).Returns(userId.ToString());
        _eventRepo
            .Setup(x => x.GetById(It.IsAny<Guid>()))
            .ReturnsAsync(
                new Event(
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    DateTime.UtcNow,
                    DateTime.UtcNow
                )
            );
        _stateUserRepo
            .Setup(x => x.GetById(It.IsAny<Guid>()))
            .ReturnsAsync(new StateUser(UserId: userId, StateId: Guid.NewGuid(), Rate: 1.0));

        await _service.validateCanUse(
            new Lesson(Guid.NewGuid(), Guid.NewGuid())
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
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    DateTime.UtcNow,
                    DateTime.UtcNow
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
                    new Lesson(Guid.NewGuid(), Guid.NewGuid())
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
            new Lesson(Guid.NewGuid(), Guid.NewGuid())
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
                    new Lesson(Guid.NewGuid(), Guid.NewGuid())
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
                    new Lesson(Guid.NewGuid(), id)
                )
        );
    }
}