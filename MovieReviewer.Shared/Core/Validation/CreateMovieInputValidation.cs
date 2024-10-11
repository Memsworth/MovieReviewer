using FluentValidation;
using MovieReviewer.Shared.Core.DTO.Inputs;
using MovieReviewer.Shared.Core.Enums;

namespace MovieReviewer.Shared.Core.Validation;

public class CreateMovieInputValidation : AbstractValidator<CreateMovieInputModel>
{
    public CreateMovieInputValidation()
    {
        RuleFor(model => model.Title).NotEmpty().WithMessage("Title is required");
        RuleFor(model => model.ImdbId).NotEmpty().WithMessage("ImdbId is required");
        RuleFor(model => model.MovieRating).IsInEnum().WithMessage("Movie rating is required");
        RuleFor(model => model.Plot).NotEmpty().WithMessage("Plot is required");
        RuleFor(model => model.MovieLanguage).IsInEnum().WithMessage("Movie language is required");
        RuleFor(model => model.ImdbRating).InclusiveBetween(1.0, 10.0)
            .WithMessage("ImdbRating must be between 1.0 and 10.");
    }
    
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<CreateMovieInputModel>
            .CreateWithOptions((CreateMovieInputModel)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}