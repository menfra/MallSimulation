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
            RuleFor(c => c.Id).NotEmpty().NotEqual("string");
            RuleFor(c => c.DisplayName).NotEqual("string");
            RuleFor(c => c.Name).NotEqual("string");
            RuleFor(c => c.Products).Must(HaveUniqueIds).WithMessage("List of ProductId to buy must be unique.");
            RuleFor(c => c.CurrentStandJoined).NotEqual("string");
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
