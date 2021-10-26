using System.Collections.Generic;
using System.Threading.Tasks;
using SDLCSimulator_BusinessLogic.Models.Output;

namespace SDLCSimulator_BusinessLogic.Interfaces
{
    public interface IGroupService
    {
        Task<List<GroupModel>> GetAllGroupsAsync();
        Task<List<GroupModel>> GetTeacherGroupsAsync(int teacherId);
    }
}
