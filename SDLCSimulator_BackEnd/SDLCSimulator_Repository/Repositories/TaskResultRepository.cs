using Microsoft.EntityFrameworkCore;
using SDLCSimulator_Data;
using SDLCSimulator_Repository.Interfaces;

namespace SDLCSimulator_Repository.Repositories
{
    public class TaskResultRepository : StatelessRepositoryBase<TaskResult>, ITaskResultRepository
    {
        public TaskResultRepository(IDbContextFactory<SDLCSimulatorDbContext> factory) : base(factory)
        {
        }
    }
}
