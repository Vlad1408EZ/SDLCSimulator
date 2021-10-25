using Microsoft.Extensions.DependencyInjection;
using SDLCSimulator_BusinessLogic.Interfaces;
using SDLCSimulator_BusinessLogic.Services;

namespace SDLCSimulator_BackEnd.Extensions
{
    public static class AddServices
    {
        public static void AddAppServices(this IServiceCollection collection)
        {
            collection.AddScoped<IUserService,UserService>();
            collection.AddScoped<IAuthService,AuthService>();
        }
    }
}
