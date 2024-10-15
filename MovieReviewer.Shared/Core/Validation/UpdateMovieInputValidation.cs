using FluentValidation;
using MovieReviewer.Shared.Core.DTO.Inputs;

namespace MovieReviewer.Shared.Core.Validation;

public class UpdateMovieInputValidation : AbstractValidator<UpdateMovieInputModel>
{
    public UpdateMovieInputValidation()
    {
        Include(new MovieInputValidation());
        RuleFor(model => model.Disabled).NotEmpty().WithMessage("Disabled is required");
    }
    
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<UpdateMovieInputModel>
            .CreateWithOptions((UpdateMovieInputModel)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}