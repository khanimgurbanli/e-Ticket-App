using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Models.Validators
{
    public class ActorValidator : AbstractValidator<Actor>
    {
        public ActorValidator()
        {
            RuleFor(x => x.profilPctureUrl).NotEmpty().WithMessage("Profle picture is required");
            RuleFor(x => x.FulName).NotEmpty().Length(3,100).WithMessage("Fullname is required");
            RuleFor(x => x.Bio).NotEmpty().MinimumLength(10).MaximumLength(300);
        }
    }
}
