using FluentValidation;
using SDLCSimulator_BusinessLogic.Models.General;

namespace SDLCSimulator_BusinessLogic.Validators
{
    public class AuthenticateRequestModelValidator : AbstractValidator<AuthenticateRequestModel>
    {
    public AuthenticateRequestModelValidator()
    {
        RuleFor(model => model.Email).NotNull();
        RuleFor(model => model.Email).EmailAddress();

        RuleFor(model => model.Password).NotNull();
        RuleFor(model => model.Password).MinimumLength(6);
        RuleFor(model => model.Password).MaximumLength(50);
    }
    }
}
