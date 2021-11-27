using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using SDLCSimulator_BusinessLogic.Models.Input;
using SDLCSimulator_Data.Enums;

namespace SDLCSimulator_BusinessLogic.Validators
{
    public class CreateUserInputModelValidator : AbstractValidator<CreateUserInputModel>
    {
        public CreateUserInputModelValidator()
        {
            RuleFor(model => model.FirstName).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(model => model.LastName).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(model => model.FirstName[0].ToString().ToUpper()).Equal(model => model.FirstName[0].ToString())
                .WithMessage("Ім'я має починатись з великої букви").When(model => !string.IsNullOrEmpty(model.FirstName));
            RuleFor(model => model.LastName[0].ToString().ToUpper()).Equal(model => model.LastName[0].ToString())
                .WithMessage("Прізвище має починатись з великої букви").When(model => !string.IsNullOrEmpty(model.LastName));         
            RuleFor(model => model.Groups.Count).Equal(1).When(model => model.Role == RoleEnum.Student);
            RuleFor(model => model.Groups.Count).GreaterThan(0).When(model => model.Role == RoleEnum.Teacher);
            RuleFor(model => model.Groups).Must(gr => gr.Distinct().Count() == gr.Count)
                .WithMessage("Вхідні дані про групи мають бути унікальними").When(model => model.Role == RoleEnum.Teacher);
            RuleFor(model => model.Role).NotEqual(RoleEnum.Admin);
            RuleFor(model => model.Email)
                .Equal(model => $"{model.FirstName.ToLower()}.{model.LastName.ToLower()}.викладач@lpnu.ua")
                .When(model => model.Role == RoleEnum.Teacher && !string.IsNullOrEmpty(model.FirstName) && !string.IsNullOrEmpty(model.LastName));
            RuleFor(model => model.Email)
                .Equal(model => $"{model.FirstName.ToLower()}.{model.LastName.ToLower()}.{model.Groups[0].Split('-', StringSplitOptions.TrimEntries)[0].ToLower()}@lpnu.ua")
                .When(model => model.Groups.Count > 0 && model.Role == RoleEnum.Student &&
                !string.IsNullOrEmpty(model.FirstName) && !string.IsNullOrEmpty(model.LastName));
            RuleFor(model => model.Password).NotNull();
            RuleFor(model => model.Password).MinimumLength(6);
            RuleFor(model => model.Password).MaximumLength(50);
        }
    }
}
