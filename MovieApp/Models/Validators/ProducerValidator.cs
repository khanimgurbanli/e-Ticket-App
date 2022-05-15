using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Models.Validators
{
    public class ProducerValidator : AbstractValidator<Producer>
    {
        public ProducerValidator()
        {
            RuleFor(x => x.FulName).NotEmpty().MinimumLength(5).MaximumLength(50);
            RuleFor(x => x.profilPctureUrl).NotEmpty().WithMessage("Profile picture is required");
            RuleFor(x => x.Bio).NotEmpty().WithMessage("Biography is required").MinimumLength(10).MaximumLength(200).NotEmpty();
        }
    }
}
