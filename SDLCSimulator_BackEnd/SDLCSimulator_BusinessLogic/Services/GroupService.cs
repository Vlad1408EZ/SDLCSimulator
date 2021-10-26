using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SDLCSimulator_BusinessLogic.Interfaces;
using SDLCSimulator_BusinessLogic.Models.Output;
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

        public async Task<List<GroupModel>> GetAllGroupsAsync()
        {
            var groups = await _groupRepository.GetAll().ToListAsync();

            return groups.Select(w => new GroupModel()
            {
                Id = w.Id,
                GroupName = w.GroupName
            }).ToList();
        }

        public async Task<List<GroupModel>> GetTeacherGroupsAsync(int teacherId)
        {
            var groups = await _groupRepository
                .GetByCondition(g => g.GroupTeachers.Any(gt => gt.TeacherId == teacherId)).ToListAsync();

            return groups.Select(w => new GroupModel()
            {
                Id = w.Id,
                GroupName = w.GroupName
            }).ToList();
        }
    }
}
