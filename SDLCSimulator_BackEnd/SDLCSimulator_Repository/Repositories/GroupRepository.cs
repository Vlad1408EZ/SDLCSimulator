using Microsoft.EntityFrameworkCore;
using SDLCSimulator_Data;
using SDLCSimulator_Repository.Interfaces;

namespace SDLCSimulator_Repository.Repositories
{
    public class GroupRepository : StatelessRepositoryBase<Group>, IGroupRepository
    {
        public GroupRepository(IDbContextFactory<SDLCSimulatorDbContext> factory) : base(factory)
        {
        }
    }
}
