using Microsoft.EntityFrameworkCore;
using SDLCSimulator_Data;
using SDLCSimulator_Repository.Interfaces;

namespace SDLCSimulator_Repository.Repositories
{
    public class TaskRepository : StatelessRepositoryBase<Task>, ITaskRepository
    {
        public TaskRepository(IDbContextFactory<SDLCSimulatorDbContext> factory) : base(factory)
        {
        }
    }
}
