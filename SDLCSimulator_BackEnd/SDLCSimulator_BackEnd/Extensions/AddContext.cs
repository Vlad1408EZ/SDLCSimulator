using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SDLCSimulator_Data;

namespace SDLCSimulator_BackEnd.Extensions
{
    public static class AddContext
    {
        public static void AddAppDbContext(this IServiceCollection collection, IConfiguration configuration)
        {
            collection.AddPooledDbContextFactory<SDLCSimulatorDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("ConnectionString"));
            });
        }
    }
}
