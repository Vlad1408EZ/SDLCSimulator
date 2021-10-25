using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SDLCSimulator_Data;
using SDLCSimulator_Repository.Interfaces;
using TaskModel = SDLCSimulator_Data.Task;

namespace SDLCSimulator_Repository.Repositories
{
    public class TaskRepository : StatelessRepositoryBase<TaskModel>, ITaskRepository
    {
        public TaskRepository(IDbContextFactory<SDLCSimulatorDbContext> factory) : base(factory)
        {
        }

        public IQueryable<TaskModel> GetTasksWithTaskResultsForStudent(int groupId, int userId)
        {
           return GetByCondition(t => t.GroupTasks.Any(gt => gt.GroupId == groupId))
                .Include(t => t.Teacher)
                .Include(t => t.TaskResults.Where(tr => tr.StudentId == userId));
        }
    }
}
