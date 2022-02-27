using BusinessLogics.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogics.Validators
{
    public class CustomerValidator : AbstractValidator<CustomerDTO>
    {
        public CustomerValidator()
        {
            RuleFor(c => c.Id).NotEmpty();
            RuleFor(c => c.DisplayName).NotEmpty();
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Products).Must(HaveUniqueIds).WithMessage("List of ProductId to buy must be unique.");

        }

        private bool HaveUniqueIds(List<ProductDTO> arg)
        {
            var distinctProducts = arg.Distinct();
            if (distinctProducts.Count() != arg.Count)
                return false;

            return true;

        }
    }
}
