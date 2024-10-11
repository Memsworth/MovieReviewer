using FluentValidation;
using MovieReviewer.Shared.Core.DTO.Inputs;

namespace MovieReviewer.Shared.Core.Validation;

public class UpdateMovieInputValidation : AbstractValidator<UpdateMovieInputModel>
{
    public UpdateMovieInputValidation()
    {
        RuleFor(model => model.Title).NotEmpty().WithMessage("Title is required");
        RuleFor(model => model.MovieRating).IsInEnum().WithMessage("Movie rating is required");
        RuleFor(model => model.Plot).NotEmpty().WithMessage("Plot is required");
        RuleFor(model => model.MovieLanguage).IsInEnum().WithMessage("Movie language is required");
        RuleFor(model => model.Disabled).NotEmpty().WithMessage("Disabled is required");
        RuleFor(model => model.ImdbRating).InclusiveBetween(1.0, 10.0)
            .WithMessage("ImdbRating must be between 1.0 and 10.");
    }
}