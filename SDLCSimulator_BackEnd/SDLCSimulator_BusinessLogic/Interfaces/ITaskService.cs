using System.Collections.Generic;
using System.Threading.Tasks;
using SDLCSimulator_BusinessLogic.Models.Input;
using SDLCSimulator_BusinessLogic.Models.Output;

namespace SDLCSimulator_BusinessLogic.Interfaces
{
    public interface ITaskService
    {
        Task<List<StudentTasksOutputModel>> GetFilteredTasksWithTaskResultsForStudentAsync(
            TaskForStudentFilterInput filterInput,int groupId,int userId);

        Task<List<TeacherTasksOutputModel>> GetFilteredTasksWithTaskResultsForTeacherAsync(
            TaskForTeacherFilterInput filterInput,int userId);
    }
}
