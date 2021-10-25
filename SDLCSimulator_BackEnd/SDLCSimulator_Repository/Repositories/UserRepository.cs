using Microsoft.EntityFrameworkCore;
using SDLCSimulator_Data;
using SDLCSimulator_Repository.Interfaces;

namespace SDLCSimulator_Repository.Repositories
{
    public class UserRepository : StatelessRepositoryBase<User>, IUserRepository
    {
        public UserRepository(IDbContextFactory<SDLCSimulatorDbContext> factory) : base(factory)
        {
        }
    }
}
