using eTickets.Data.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Models.Validators
{
    public class VMLoginValidator : AbstractValidator<ViewModelLogin>
    {
        public VMLoginValidator()
        {
            RuleFor(x => x.EmailAddress).NotEmpty().WithMessage("Email Address is required")
                .EmailAddress().WithMessage("Invalid email format.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required")
                .Length(8, 20).WithMessage("The length of the Password must be a minimum of 8 and a maximum of 20 characters")
            .Matches("[^a-zA-Z0-9]").WithMessage("Invalid format");
        }
    }
}
