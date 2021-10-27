using Microsoft.EntityFrameworkCore;
using SDLCSimulator_Data;
using SDLCSimulator_Repository.Interfaces;

namespace SDLCSimulator_Repository.Repositories
{
    public class GroupTaskRepository : StatelessRepositoryBase<GroupTask>, IGroupTaskRepository
    {
        public GroupTaskRepository(IDbContextFactory<SDLCSimulatorDbContext> factory) : base(factory)
        {
        }
    }
}
