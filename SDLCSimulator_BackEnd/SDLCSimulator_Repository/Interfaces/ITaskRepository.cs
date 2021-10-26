using System.Linq;
using TaskModel = SDLCSimulator_Data.Task;

namespace SDLCSimulator_Repository.Interfaces
{
    public interface ITaskRepository : IStatelessRepository<TaskModel>
    {
        IQueryable<TaskModel> GetTasksWithTaskResultsForStudent(int groupId,int userId);
        IQueryable<TaskModel> GetTasksWithTaskResultsForTeacher(int userId);
    }
}
