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
            RuleFor(c => c.Id).NotEmpty();
            RuleFor(c => c.DisplayName).NotEmpty();
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.OpenClosedDuration).GreaterThan(0);
            RuleFor(c => c.OpenedState).Must(BeEitherOpenOrClosed); ; //Not sure
        }

        private bool BeEitherOpenOrClosed(States arg)
        {
            return Enum.IsDefined(typeof(States), arg.ToString());
        }
    }
}
