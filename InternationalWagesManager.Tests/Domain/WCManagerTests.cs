using AutoMapper;
using InternationalWagesManager.Domain;

namespace InternationalWagesManager.Tests.WorkConditions;

[TestFixture]
public class WCManagerTests
{
    private Mock<IWConditionsRepository> _mockRepo;
    private WorkConditionsManager _workConditionsManager;
    private Web.Mapper _myProfileMapper = new();
    private AutoMapper.Mapper _mapper;


    [SetUp]
    public void Setup()
    {
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(_myProfileMapper));
        _mapper = new AutoMapper.Mapper(configuration);
        _mockRepo = new Mock<IWConditionsRepository>();
        _mockRepo.Setup(mr => mr.GetAllEmployeesWCAsync(It.IsAny<int>())).ReturnsAsync(GetEmployeeWorkConditions());
        _workConditionsManager = new WorkConditionsManager(_mapper, _mockRepo.Object);
    }


    [Test]
    public void AddWorkConditions_IfValid_TheAddMethodOfTheRepositoryShouldBeCould()
    {
        //set
        var dtoWorkConditons = new DTO.WorkConditions() { EmployeeId = 1, PayRate = 34 };

        //act
        _workConditionsManager.AddWorkConditions(dtoWorkConditons);

        //assert
        _mockRepo.Verify(mr => mr.AddWorkConditions(It.IsAny<Models.WorkConditions>()));
    }

    [Test]
    [TestCase(0, 34)]
    [TestCase(1, 0)]
    public void AddWorkConditions_IfInvalid_TheRepositoryAddMethodShouldBeCalled(int employeeId, int payRate)
    {
        //set
        var dtoWorkConditons = new DTO.WorkConditions() { EmployeeId = employeeId, PayRate = payRate };

        //act
        _workConditionsManager.AddWorkConditions(dtoWorkConditons);

        //assert
        _mockRepo.Verify(mr => mr.AddWorkConditions(It.IsAny<Models.WorkConditions>()), Times.Never());
    }
    [Test]
    public void UpdateWorkConditionsAsync_IfValidId_TheRepositoryUpdateMethodShoudBeCalled()
    {
        var dtoWorkConditions = new DTO.WorkConditions() { Id = 1 };
        _workConditionsManager.UpdateWorkConditionsAsync(dtoWorkConditions).Wait();
        _mockRepo.Verify(mr => mr.UpdateWorkConditionsAsync(It.IsAny<Models.WorkConditions>()));
    }
    [Test]
    public void UpdateWorkConditionsAsync_IfInvalidId_TheRepositoryUpdateMethodShoudNotBeCalled()
    {
        var dtoWorkConditions = new DTO.WorkConditions() { Id = 0 };
        _workConditionsManager.UpdateWorkConditionsAsync(dtoWorkConditions).Wait();
        _mockRepo.Verify(mr => mr.UpdateWorkConditionsAsync(It.IsAny<Models.WorkConditions>()), Times.Never);
    }

    [Test]
    public async Task DeleteWorkConditionsAsync_IfInvalidId_ReturnsFalse()
    {
        var result = await _workConditionsManager.DeleteWorkConditionsAsync(0);
        Assert.That(result == false);
    }

    [Test]
    public async Task LatestWorkConditions_IfEmployeeExsits_ShouldReturnMostRecentConditions()
    {
        var result = await _workConditionsManager.LatestWorkConditions(1);

        Assert.That(result.Date == new DateTime(2022, 05, 01));
    }

    [Test]
    public async Task WorkConditionsToDateAsync_IfDateExists_ReturnsWCForThatDate()
    {
        var result = await _workConditionsManager.WorkConditionsToDateAsync(1, new DateTime(2022, 01, 01));
        var expectedResult = _mapper.Map<DTO.WorkConditions>(GetEmployeeWorkConditions()[0]);
        Assert.That(result.Id == expectedResult.Id);
    }

    [Test]
    public async Task WorkConditionsToDateAsync_IfOnlyYearAndMonthExists_ReturnsLatestWCOfThatMonth()
    {

        var result = await _workConditionsManager.WorkConditionsToDateAsync(1, new DateTime(2022, 01, 07));
        var expectedResult = _mapper.Map<DTO.WorkConditions>(GetEmployeeWorkConditions()[1]);
        Assert.That(result.Id == expectedResult.Id);
    }

    [Test]
    public async Task WorkConditionsToDateAsync_IfOnlyYearExists_ReturnsLatestWCOfThatYear()
    {

        var result = await _workConditionsManager.WorkConditionsToDateAsync(1, new DateTime(2022, 03, 07));
        var expectedResult = _mapper.Map<DTO.WorkConditions>(GetEmployeeWorkConditions()[2]);
        Assert.That(result.Id == expectedResult.Id);
    }
    [Test]
    public async Task WorkConditionsToDateAsync_IfNoPartOfDateExists_ReturnsLatestWC()
    {

        var result = await _workConditionsManager.WorkConditionsToDateAsync(1, new DateTime(2023, 01, 01));
        var expectedResult = _mapper.Map<DTO.WorkConditions>(GetEmployeeWorkConditions()[2]);
        Assert.That(result.Id == expectedResult.Id);
    }


    private List<Models.WorkConditions> GetEmployeeWorkConditions()
    {
        var employee = new Models.Employee
        {
            Id = 1,
            DOB = new DateTime(1984, 04, 06),
            FirstName = "osher",
            LastName = "moscovitch",
            Email = "oa@.com",
            Phone = "02088060372"
        };

        return new List<Models.WorkConditions>
        {
            new Models.WorkConditions()
            {
                Id = 1,
                Date = new DateTime(2022, 01, 01),
                PayRate = 45.6f,
                Deductions = 5,
                EmployeeId = 1,
                Employee =  employee
            },
            new Models.WorkConditions()
            {
                Id = 2,
                Date = new DateTime(2022, 01, 05),
                PayRate = 45.6f,
                Deductions = 0,
                EmployeeId = 1,
                Employee= employee
            },
            new Models.WorkConditions()
            {
                Id = 3,
                Date = new DateTime(2022, 05, 01),
                PayRate = 45.6f,
                Deductions = 5,
                EmployeeId = 1,
                Employee =  employee
            }
            ,new Models.WorkConditions()
            {
                Id = 4,
                Date = new DateTime(2021, 01, 01),
                PayRate = 45.6f,
                Deductions = 5,
                EmployeeId = 1,
                Employee =  employee
            },

        };
    }
}