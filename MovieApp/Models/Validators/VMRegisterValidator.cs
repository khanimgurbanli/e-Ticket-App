using eTickets.Data.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Models.Validators
{
    public class VMRegisterValidator : AbstractValidator<ViewModelRegister>
    {
        public VMRegisterValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Fullname is required")
             .Matches("[A-Z]")
            .Matches("[a-z]")
                .WithMessage("Invalid format.");
            RuleFor(x => x.EmailAddress).NotEmpty().WithMessage("Email Address is required")
              .EmailAddress().WithMessage("Invalid email format.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required")
                .Length(8, 20).WithMessage("The password mixture must consist of a minimum of 8 and a maximum of 20 characters")
            .Matches("[^a-zA-Z0-9]").WithMessage("Invalid format");
            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Passwords don't match")
               .Length(8, 20).WithMessage("The password mixture must consist of a minimum of 8 and a maximum of 20 characters")
               .Equal(x => x.ConfirmPassword)
               .When(x => !String.IsNullOrWhiteSpace(x.Password))
           .Matches("[^a-zA-Z0-9]").WithMessage("Invalid format");
        }
    }
}
