using FluentValidation;
using MovieReviewer.Shared.Dto.Input;

namespace MovieReviewer.Service.Validation;

public class UpdateMovieDtoValidation : AbstractValidator<UpdateMovieDto>
{
    public UpdateMovieDtoValidation()
    {
        RuleFor(model => model.Title).NotEmpty().WithMessage("Title is required");
        RuleFor(model => model.MovieRating).IsInEnum().WithMessage("Movie rating is required");
        RuleFor(model => model.Plot).NotEmpty().WithMessage("Plot is required");
        RuleFor(model => model.MovieLanguage).IsInEnum().WithMessage("Movie language is required");
        RuleFor(model => model.ImdbRating).InclusiveBetween(1.0, 10.0)
            .WithMessage("ImdbRating must be between 1.0 and 10.");
        RuleFor(model => model.Disabled).NotEmpty().WithMessage("Disabled is required");
    }
}