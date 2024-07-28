using System;
using System.Threading.Tasks;
using BackendBase.Dto;
using BackendBase.Dto.CreateDto;
using BackendBase.Interfaces.Repositories;
using BackendBase.Interfaces.Services;
using BackendBase.Models;
using BackendBase.Services;
using Moq;
using NUnit.Framework;

namespace TsuImportTests.Services;

[TestFixture]
public class StateServiceTests
{
    public StateServiceTests()
    {
        _service = new StateService(
            _stateRepo.Object,
            _departmentRepo.Object,
            _userRepo.Object,
            _stateUserRepo.Object
        );
    }

    private readonly Mock<IStateRepository> _stateRepo = new();
    private readonly Mock<IStateUserRepository> _stateUserRepo = new();
    private readonly Mock<IUserRepository> _userRepo = new();
    private readonly Mock<IDepartmentRepository> _departmentRepo = new();
    private readonly IStateService _service;

    [SetUp]
    public void MockReset()
    {
        _stateRepo.Reset();
        _stateUserRepo.Reset();
    }

    [Test]
    public async Task STATE_Create()
    {
        var dto = new StateCreateDto
        {
            JobId = Guid.NewGuid().ToString(),
            Count = 1,
            Hours = 1485,
            EndDate = new System.DateTime(2024, 1, 1),
            StartDate = new System.DateTime(2023, 12, 31)
        };

        _departmentRepo.Setup(x => x.Search(It.IsAny<SearchDto>())).ReturnsAsync(new Pagination<Department>
        {
            PageSize = 1,
            PageNumber = 1,
            TotalPages = 1,
            Entities = new Department[] { new Department { Id = Guid.NewGuid() } }
        });
        _stateRepo.Setup(x => x.AddEntity(It.IsAny<State>())).ReturnsAsync((State x) => x);

        var stateId = await _service.Create(dto);

        Assert.Pass("No errors was thrown");
    }

    [Test]
    public async Task ASSIGN_Success()
    {
        var userId = Guid.NewGuid();
        var stateId = Guid.NewGuid();

        _stateRepo.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(new State { Id = stateId, Count = 1 });
        _userRepo.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(new User { Id = userId });

        var dto = new StateUserCreateDto
        {
            UserId = userId.ToString(),
            StateId = stateId.ToString()
        };

        var success = await _service.Assign(dto);

        Assert.True(success);
    }

    [Test]
    public async Task ASSIGN_CountIsLessThanOne()
    {
        var userId = Guid.NewGuid();
        var stateId = Guid.NewGuid();

        _stateRepo.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(new State { Id = stateId, Count = 0 });
        _userRepo.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(new User { Id = userId });

        var dto = new StateUserCreateDto
        {
            UserId = userId.ToString(),
            StateId = stateId.ToString()
        };

        var success = await _service.Assign(dto);

        Assert.False(success);
    }
}
