using FluentValidation;
using SDLCSimulator_BusinessLogic.Models.General;

namespace SDLCSimulator_BusinessLogic.Validators
{
    public class ChangePasswordRequestModelValidator : AbstractValidator<ChangePasswordRequestModel>
    {
        public ChangePasswordRequestModelValidator()
        {
            RuleFor(model => model.OldPassword).NotEqual(model => model.NewPassword);
            RuleFor(model => model.NewPassword).NotNull();
            RuleFor(model => model.NewPassword).MinimumLength(6);
            RuleFor(model => model.NewPassword).MaximumLength(50);
            RuleFor(model => model.ConfirmPassword).Equal(model => model.NewPassword);
        }
    }
}
