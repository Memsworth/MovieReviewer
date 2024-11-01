using FluentValidation;
using MovieReviewer.Core.DTO.Inputs;

namespace MovieReviewer.Service.Validation;

public class MovieInputValidation : AbstractValidator<MovieInputModel>
{
    public MovieInputValidation()
    {
        RuleFor(model => model.Title).NotEmpty().WithMessage("Title is required");
        RuleFor(model => model.MovieRating).IsInEnum().WithMessage("Movie rating is required");
        RuleFor(model => model.Plot).NotEmpty().WithMessage("Plot is required");
        RuleFor(model => model.MovieLanguage).IsInEnum().WithMessage("Movie language is required");
        RuleFor(model => model.ImdbRating).InclusiveBetween(1.0, 10.0)
            .WithMessage("ImdbRating must be between 1.0 and 10.");
    }
}