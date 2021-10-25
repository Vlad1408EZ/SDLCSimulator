using Microsoft.Extensions.DependencyInjection;
using SDLCSimulator_Repository.Interfaces;
using SDLCSimulator_Repository.Repositories;

namespace SDLCSimulator_BackEnd.Extensions
{
    public static class AddRepositories
    {
        public static void AddAppRepositories(this IServiceCollection collection)
        {
            collection.AddScoped<IGroupRepository,GroupRepository>();
            collection.AddScoped<ITaskRepository, TaskRepository>();
            collection.AddScoped<ITaskResultRepository, TaskResultRepository>();
            collection.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
