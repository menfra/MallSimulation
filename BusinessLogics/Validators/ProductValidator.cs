using BusinessLogics.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogics.Validators
{
    public class ProductValidator : AbstractValidator<ProductDTO>
    {
        public ProductValidator()
        {
            RuleFor(c => c.Id).NotEmpty();
            RuleFor(c => c.DisplayName).NotEmpty();
            RuleFor(c => c.Name).NotEmpty();
        }
    }
}
