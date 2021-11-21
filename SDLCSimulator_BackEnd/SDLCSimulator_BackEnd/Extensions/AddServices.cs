using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SDLCSimulator_BusinessLogic.Interfaces;
using SDLCSimulator_BusinessLogic.Services;
using SDLCSimulator_Repository.Interfaces;

namespace SDLCSimulator_BackEnd.Extensions
{
    public static class AddServices
    {
        public static void AddAppServices(this IServiceCollection collection, IConfiguration configuration)
        {
            collection.AddScoped<IUserService,UserService>();
            collection.AddScoped<IAuthService,AuthService>();
            collection.AddScoped<IDifficultyService,DifficultyService>();
            collection.AddScoped<ITypeService,TypeService>();
            collection.AddScoped<ITaskService,TaskService>();
            collection.AddScoped<ITaskResultService,TaskResultService>();
            collection.AddScoped<IRoleService,RoleService>();
            collection.AddScoped<IGroupService,GroupService>();
            collection.AddScoped<IGradeCalculator, RequirementsTypeAndOrderByImportanceTaskCalculator>();
            collection.AddScoped<IGradeCalculator, SystemsTypeAndFindMostImportantTaskCalculator>();
            collection.AddScoped<IEmailService,EmailService>(sp => new EmailService(configuration,
                sp.GetRequiredService<IUserRepository>()));
        }
    }
}
