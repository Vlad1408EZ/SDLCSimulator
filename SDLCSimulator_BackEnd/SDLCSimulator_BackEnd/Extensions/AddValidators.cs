using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SDLCSimulator_BusinessLogic.Models.General;
using SDLCSimulator_BusinessLogic.Models.Input;
using SDLCSimulator_BusinessLogic.Validators;

namespace SDLCSimulator_BackEnd.Extensions
{
    public static class AddValidators
    {
        public static void AddAppValidators(this IServiceCollection collection)
        {
            collection.AddScoped<IValidator<AuthenticateRequestModel>, AuthenticateRequestModelValidator>();
            collection.AddScoped<IValidator<ChangePasswordRequestModel>,ChangePasswordRequestModelValidator>();
            collection.AddScoped<IValidator<GroupInputModel>,GroupInputModelValidator>();
        }
    }
}
