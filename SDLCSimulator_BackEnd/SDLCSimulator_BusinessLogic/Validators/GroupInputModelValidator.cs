using FluentValidation;
using SDLCSimulator_BusinessLogic.Models.Input;

namespace SDLCSimulator_BusinessLogic.Validators
{
    public class GroupInputModelValidator : AbstractValidator<GroupInputModel>
    {
        public GroupInputModelValidator()
        {
            RuleFor(model => model.GroupName).Matches("^[А-Я]{1,4}-[0-9]{2}$");
        }
    }
}
