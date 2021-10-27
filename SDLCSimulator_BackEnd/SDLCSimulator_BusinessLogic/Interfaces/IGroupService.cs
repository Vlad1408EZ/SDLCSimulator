using System.Collections.Generic;
using System.Threading.Tasks;
using SDLCSimulator_BusinessLogic.Models.Input;
using SDLCSimulator_BusinessLogic.Models.Output;

namespace SDLCSimulator_BusinessLogic.Interfaces
{
    public interface IGroupService
    {
        Task<List<GroupOutputModel>> GetAllGroupsAsync();
        Task<List<GroupOutputModel>> GetTeacherGroupsAsync(int teacherId);
        Task<GroupOutputModel> CreateGroupAsync(GroupInputModel model);
    }
}
