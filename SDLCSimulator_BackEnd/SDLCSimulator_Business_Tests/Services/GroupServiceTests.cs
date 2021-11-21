using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using SDLCSimulator_BusinessLogic.Models.Output;
using SDLCSimulator_BusinessLogic.Services;
using SDLCSimulator_Common;
using SDLCSimulator_Common.Fixtures;
using SDLCSimulator_Data;
using SDLCSimulator_Repository.Interfaces;
using SDLCSimulator_Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SDLCSimulator_BusinessLogic_Tests.Services
{
    public class GroupServiceTests
    {
        private readonly SDLCSimulatorDbContext _dbContext;
        private readonly GroupRepository _groupRepository;
        private readonly GroupService _groupService;

        public GroupServiceTests()
        {
            var options = new DbContextOptionsBuilder<SDLCSimulatorDbContext>()
                .UseInMemoryDatabase(databaseName: "GroupTests").Options;
            _dbContext = new SDLCSimulatorDbContext(options);
            var _dbContextFactory = new Mock<IDbContextFactory<SDLCSimulatorDbContext>>();
            _dbContextFactory.Setup(d => d.CreateDbContext()).Returns(_dbContext);
            _groupRepository = new GroupRepository(_dbContextFactory.Object);
            _groupService = new GroupService(_groupRepository);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetAllGroupsAsync_ReturnAllGroups()
        {
            //arrange
            _dbContext.ClearDb();
            _dbContext.InMemoryDatabaseSetup();
            var output = new List<GroupOutputModel>
            {
                new GroupOutputModel{ Id = 1, GroupName = "ПЗ-41" },
                new GroupOutputModel{ Id = 2, GroupName = "ПЗ-42" },
                new GroupOutputModel{ Id = 3, GroupName = "ПЗ-43" },
                new GroupOutputModel{ Id = 4, GroupName = "ПЗ-44" },
                new GroupOutputModel{ Id = 5, GroupName = "ПЗ-45" }
            };

            //act
            var res = await _groupService.GetAllGroupsAsync();

            //assert
            res.Should().BeEquivalentTo(output);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetTeacherGroupsAsync_ExistingTeacherId_ReturnTeacherGroups()
        {
            //arrange
            _dbContext.ClearDb();
            _dbContext.InMemoryDatabaseSetup();
            var output = new List<GroupOutputModel>
            {
                new GroupOutputModel{ Id = 1, GroupName = "ПЗ-41" },
                new GroupOutputModel{ Id = 2, GroupName = "ПЗ-42" }
            };

            //act
            var res = await _groupService.GetTeacherGroupsAsync(2);

            //assert
            res.Should().BeEquivalentTo(output);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetTeacherGroupsAsync_NotExistingTeacherId_ReturnEmptyCollection()
        {
            //arrange
            _dbContext.ClearDb();
            _dbContext.InMemoryDatabaseSetup();

            //act
            var res = await _groupService.GetTeacherGroupsAsync(1);

            //assert
            res.Should().BeEquivalentTo(new List<GroupOutputModel>());
        }

        [Fact]
        public async System.Threading.Tasks.Task CreateGroupAsyncAsync_ReturnCreatedGroup()
        {
            //arrange
            _dbContext.ClearDb();
            _dbContext.InMemoryDatabaseSetup();
            var group = CreateGroupInputModelFixture.CreateValidEntity();
            group.GroupName = "ПЗ-46";

            //act
            var res = await _groupService.CreateGroupAsync(group);

            //assert
            res.GroupName.Should().Be("ПЗ-46");
        }

        [Fact]
        public async System.Threading.Tasks.Task CreateGroupAsyncAsync_GroupAlreadyExists_ThrowsException()
        {
            //arrange
            _dbContext.ClearDb();
            _dbContext.InMemoryDatabaseSetup();
            var group = CreateGroupInputModelFixture.CreateValidEntity();

            //act
            Func<Task<GroupOutputModel>> func = () => _groupService.CreateGroupAsync(group);

            //assert
            await func.Should().ThrowAsync<InvalidOperationException>().WithMessage($"Група 'ПЗ-41' вже існує");
        }
    }
}
