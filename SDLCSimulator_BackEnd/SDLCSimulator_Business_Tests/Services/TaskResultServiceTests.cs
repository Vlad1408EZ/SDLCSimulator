using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Newtonsoft.Json;
using SDLCSimulator_BusinessLogic.Interfaces;
using SDLCSimulator_BusinessLogic.Models.Output;
using SDLCSimulator_BusinessLogic.Services;
using SDLCSimulator_Common;
using SDLCSimulator_Common.Fixtures;
using SDLCSimulator_Data;
using SDLCSimulator_Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SDLCSimulator_BusinessLogic_Tests.Services
{
    public class TaskResultServiceTests
    {
        private readonly SDLCSimulatorDbContext _dbContext;
        private readonly TaskRepository _taskRepository;
        private readonly TaskResultRepository _taskResultRepository;
        private readonly TaskResultService _taskResultService;

        public TaskResultServiceTests()
        {
            var options = new DbContextOptionsBuilder<SDLCSimulatorDbContext>()
               .UseInMemoryDatabase(databaseName: "TaskResultTests").Options;
            _dbContext = new SDLCSimulatorDbContext(options);
            var _dbContextFactory = new Mock<IDbContextFactory<SDLCSimulatorDbContext>>();
            _dbContextFactory.Setup(d => d.CreateDbContext()).Returns(_dbContext);
            _taskRepository = new TaskRepository(_dbContextFactory.Object);
            _taskResultRepository = new TaskResultRepository(_dbContextFactory.Object);
            _taskResultService = new TaskResultService(_taskRepository, _taskResultRepository);
        }

        [Fact]
        public async System.Threading.Tasks.Task SetTaskResultAsync_RequirementsTypeAndOrderByImportanceTask_CreatedTaskResult()
        {
            //arrange
            _dbContext.ClearDb();
            _dbContext.InMemoryDatabaseSetup();
            var input = CreateTaskResultInputFixture.CreateRequirementsTypeAndOrderByImportanceTaskResult();

            //act
            var result = await _taskResultService.SetTaskResultAsync(input, 1);

            //assert
            _taskResultService.GetCalculatorType().Should().BeAssignableTo<RequirementsTypeAndOrderByImportanceTaskCalculator>();
            result.ErrorCount.Should().Be(2);
            result.Result.Should().Be(JsonConvert.SerializeObject(input.Result));
            result.Percentage.Should().Be(0.4m);
            result.FinalMark.Should().Be(14);
        }

        [Fact]
        public async System.Threading.Tasks.Task SetTaskResultAsync_RequirementsTypeAndOrderByImportanceTask_NoCorrectAnswers_FinalMarkIsZero()
        {
            //arrange
            _dbContext.ClearDb();
            _dbContext.InMemoryDatabaseSetup();
            var input = CreateTaskResultInputFixture.CreateZeroFinalMarkRequirementsTypeAndOrderByImportanceTaskResult(); 

            //act
            var result = await _taskResultService.SetTaskResultAsync(input, 1);

            //assert
            _taskResultService.GetCalculatorType().Should().BeAssignableTo<RequirementsTypeAndOrderByImportanceTaskCalculator>();
            result.ErrorCount.Should().Be(2);
            result.Result.Should().Be(JsonConvert.SerializeObject(input.Result));
            result.Percentage.Should().Be(0);
            result.FinalMark.Should().Be(0);
        }

        [Fact]
        public async System.Threading.Tasks.Task SetTaskResultAsync_SystemsTypeAndFindMostImportantTaskCalculator_CreatedTaskResult()
        {
            //arrange
            _dbContext.ClearDb();
            _dbContext.InMemoryDatabaseSetup();
            var input = CreateTaskResultInputFixture.CreateSystemsTypeAndFindMostImportantTaskResult();

            //act
            var result = await _taskResultService.SetTaskResultAsync(input, 1);

            //assert
            _taskResultService.GetCalculatorType().Should().BeAssignableTo<SystemsTypeAndFindMostImportantTaskCalculator>();
            result.ErrorCount.Should().Be(3);
            result.Result.Should().Be(JsonConvert.SerializeObject(input.Result));
            Math.Round(result.Percentage, 2).Should().Be(0.78m);
            Math.Round(result.FinalMark, 2).Should().Be(45.77m);
        }

        [Fact]
        public async System.Threading.Tasks.Task SetTaskResultAsync_SystemsTypeAndFindMostImportantTaskCalculator_NoCorrectAnswers_FinalMarkIsZero()
        {
            //arrange
            _dbContext.ClearDb();
            _dbContext.InMemoryDatabaseSetup();
            var input = CreateTaskResultInputFixture.CreateZeroFinalMarkSystemsTypeAndFindMostImportantTaskResult();

            //act
            var result = await _taskResultService.SetTaskResultAsync(input, 1);

            //assert
            _taskResultService.GetCalculatorType().Should().BeAssignableTo<SystemsTypeAndFindMostImportantTaskCalculator>();
            result.ErrorCount.Should().Be(3);
            result.Result.Should().Be(JsonConvert.SerializeObject(input.Result));
            result.Percentage.Should().Be(0);
            result.FinalMark.Should().Be(0);
        }

        [Fact]
        public async System.Threading.Tasks.Task SetTaskResultAsync_NoTaskFound_ThrowsException()
        {
            //arrange
            _dbContext.ClearDb();
            _dbContext.InMemoryDatabaseSetup();
            var input = CreateTaskResultInputFixture.CreateRequirementsTypeAndOrderByImportanceTaskResult();
            input.TaskId = 5;

            //act
            Func<Task<StudentTaskResultOutputModel>> func = () => _taskResultService.SetTaskResultAsync(input, 1);

            //assert
            await func.Should().ThrowAsync<InvalidOperationException>("Завдання з айді 5 не знайдено");
        }
    }
}
