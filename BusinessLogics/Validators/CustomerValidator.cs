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
            RuleFor(c => c.Products).Must(HaveNoStrings).WithMessage("Id, Name, DisplayName must not contain 'string' as input or empty");
        }

        private bool HaveNoStrings(List<ProductDTO> arg)
        {
            if (arg.Any(a => 
            a.Id.Equals("string") &&
            a.Id.Equals(string.Empty) &&
            a.Name.Equals("string") &&
            a.Name.Equals(string.Empty) &&
            a.DisplayName.Equals(string.Empty) &&
            a.DisplayName.Equals("string")))
                return false;

            return true;
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
