using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Models.Validators
{
    public class CinemaValidator : AbstractValidator<Cinema>
    {
        public CinemaValidator()
        {
            RuleFor(x => x.Logo).NotEmpty().WithMessage("Logo is required");
            RuleFor(x => x.Name).NotEmpty().NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Description).NotEmpty().MaximumLength(300).MinimumLength(10);
        }
    }
}
