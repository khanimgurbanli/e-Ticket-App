using eTickets.Data.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Models.Validators
{
    public class MovieValidator : AbstractValidator<ViewModelNewMovies>
    {
        public MovieValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Movie name is required").MinimumLength(5).MaximumLength(50);
            RuleFor(x => x.Description).NotEmpty().MinimumLength(10).MaximumLength(300);
            RuleFor(x => x.Price).NotEmpty().WithMessage("Movie price is required");
            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("Movie post card is required");
            RuleFor(x => x.BeginDate).NotEmpty().WithMessage("Movie begin date is required");
            RuleFor(x => x.ProducerId).NotEmpty().WithMessage("Producer name is required");
            RuleFor(x => x.EndDate).NotEmpty().WithMessage("Include cinema is required");
            RuleFor(x => x.movieCategory).NotEmpty().IsInEnum().WithMessage("Movie category is required");
        }
    }
}
