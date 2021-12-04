using FluentValidation;
using SDLCSimulator_BusinessLogic.Models.General;

namespace SDLCSimulator_BusinessLogic.Validators
{
    public class UpdateUserInfoModelValidator : AbstractValidator<UpdateUserInfoModel>
    {
        public UpdateUserInfoModelValidator()
        {
            RuleFor(model => model.FirstName).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(model => model.LastName).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(model => model.FirstName[0].ToString().ToUpper()).Equal(model => model.FirstName[0].ToString())
                .WithMessage("Ім'я має починатись з великої букви").When(model => !string.IsNullOrEmpty(model.FirstName));
            RuleFor(model => model.LastName[0].ToString().ToUpper()).Equal(model => model.LastName[0].ToString())
                .WithMessage("Прізвище має починатись з великої букви").When(model => !string.IsNullOrEmpty(model.LastName));
        }
    }
}
