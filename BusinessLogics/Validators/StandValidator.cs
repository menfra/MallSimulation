using BusinessLogics.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogics.Validators
{
    public class StandValidator : AbstractValidator<StandDTO>
    {
        public StandValidator()
        {
            RuleFor(c => c.Id).NotEmpty().NotEqual("string");
            RuleFor(c => c.DisplayName).NotEqual("string");
            RuleFor(c => c.Name).NotEqual("string");
            RuleFor(c => c.Duration).GreaterThan(0);
            RuleFor(c => c.Product.Id).NotEqual("string");
        }
    }
}
