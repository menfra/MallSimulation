using BusinessLogics.DTO;
using DataAcess.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogics.Validators
{
    public class MallValidator : AbstractValidator<MallDTO>
    {
        public MallValidator()
        {
            RuleFor(c => c.Id).NotEmpty().NotEqual("string");
            RuleFor(c => c.DisplayName).NotEqual("string");
            RuleFor(c => c.Name).NotEqual("string");
            RuleFor(c => c.OpenClosedDuration).GreaterThan(0);
            RuleFor(c => c.OpenedState).Must(BeEitherOpenOrClosed).WithMessage("The mall can only be either Opened or Closed.");
        }

        private bool BeEitherOpenOrClosed(States arg)
        {
            return Enum.IsDefined(typeof(States), arg.ToString());
        }
    }
}
