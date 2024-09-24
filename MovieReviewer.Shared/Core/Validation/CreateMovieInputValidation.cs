using FluentValidation;
using MovieReviewer.Shared.Core.DTO.Inputs;

namespace MovieReviewer.Shared.Core.Validation;

public class CreateMovieInputValidation : AbstractValidator<CreateMovieInputModel>
{
    public CreateMovieInputValidation()
    {
        RuleFor(model => model.Title).NotEmpty().WithMessage("Title is required");
        RuleFor(model => model.ImdbId).NotEmpty().WithMessage("ImdbId is required");
        RuleFor(model => model.MovieRating).NotEmpty().WithMessage("Movie rating is required");
        RuleFor(model => model.Plot).NotEmpty().WithMessage("Plot is required");
        RuleFor(model => model.MovieLanguage).NotEmpty().WithMessage("Movie language is required");
        RuleFor(model => model.ImdbRating).InclusiveBetween(1.0, 10.0)
            .WithMessage("ImdbRating must be between 1.0 and 10.");
    }
}