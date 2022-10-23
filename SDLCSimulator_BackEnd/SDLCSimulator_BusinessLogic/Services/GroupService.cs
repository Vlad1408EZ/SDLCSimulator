using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SDLCSimulator_BusinessLogic.Interfaces;
using SDLCSimulator_BusinessLogic.Models.Input;
using SDLCSimulator_BusinessLogic.Models.Output;
using SDLCSimulator_Data;
using SDLCSimulator_Repository.Interfaces;

namespace SDLCSimulator_BusinessLogic.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;

        public GroupService(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<List<GroupOutputModel>> GetAllGroupsAsync()
        {
            var groups = await _groupRepository.GetAll().ToListAsync();
            var result = new List<GroupOutputModel>();

            foreach(var group in groups)
            {
                result.Add(new GroupOutputModel()
                {
                    Id = group.Id,
                    GroupName = group.GroupName
                });
            }

            return result;
        }

        public async Task<List<GroupOutputModel>> GetTeacherGroupsAsync(int teacherId)
        {
            var groups = await _groupRepository
                .GetByCondition(g => g.GroupTeachers.Any(gt => gt.TeacherId == teacherId)).ToListAsync();

            return groups.Select(w => new GroupOutputModel()
            {
                Id = w.Id,
                GroupName = w.GroupName
            }).ToList();
        }

        public async Task<GroupOutputModel> CreateGroupAsync(CreateGroupInputModel model)
        {
            var group = await _groupRepository.GetSingleByConditionAsync(g => g.GroupName == model.GroupName);
            if (group != null)
            {
                throw new InvalidOperationException($"Група '{model.GroupName}' вже існує");
            }

            group = new Group()
            {
                GroupName = model.GroupName
            };

            await _groupRepository.CreateAsync(group);

            return new GroupOutputModel()
            {
                Id = group.Id,
                GroupName = group.GroupName
            };
        }
    }
}
